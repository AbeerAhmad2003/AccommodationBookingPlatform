using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Contracts.Services.Pricing;
using AccommodationBookingPlatform.Application.Email;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Entities;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler
       : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IBookingPricingService _pricingService;
        private readonly ICurrentUserService _currentUser;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IEmailService _emailService;

        public CreateBookingCommandHandler(
            IBookingRepository bookingRepository,
            IHotelRepository hotelRepository,
            IBookingPricingService pricingService,
            ICurrentUserService currentUser,
            IInvoiceRepository invoiceRepository,
            IEmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _hotelRepository = hotelRepository;
            _pricingService = pricingService;
            _currentUser = currentUser;
            _invoiceRepository = invoiceRepository;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(
            CreateBookingCommand request,
            CancellationToken cancellationToken)
        {
            // Ensure user logged in
            if (_currentUser.UserId is null)
                throw new UnauthorizedAccessException("User not logged in.");

            var userId = _currentUser.UserId.Value;

            // Check hotel exists
            var hotel = await _hotelRepository.GetByIdAsync(
                request.HotelId, cancellationToken);

            if (hotel is null)
                throw new NotFoundException("Hotel", request.HotelId);

            // Check Availability
            var isAvailable = await _bookingRepository.IsHotelAvailableAsync(
                request.HotelId,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                cancellationToken);

            if (!isAvailable)
                throw new BadRequestException(
                    "Hotel is not available for the selected dates and rooms count.");

            // Pricing (supports discounts)
            var totalPrice = await _pricingService.CalculateTotalPriceAsync(
                hotel,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                request.Adults,
                request.Children,
                cancellationToken);

            // Create Booking
            var booking = new Booking
            {
                UserId = userId,
                HotelId = request.HotelId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                Adults = request.Adults,
                Children = request.Children,
                PaymentMethod = request.PaymentMethod,
                TotalPrice = totalPrice,
                CreatedAtUtc = DateTime.UtcNow
            };

            booking.SetRoomsCount(request.RoomsCount);

            booking = await _bookingRepository.CreateAsync(booking, cancellationToken);

            // Create Invoice Record
            var nowUtc = DateTime.UtcNow;

            var invoice = new InvoiceRecord
            {
                BookingId = booking.Id,
                UserId = userId,
                TotalAmount = totalPrice,
                PaymentMethod = request.PaymentMethod,

                InvoiceNumber = $"INV-{nowUtc:yyyyMMdd}-{booking.Id.ToString()[..8]}",
                CreatedAtUtc = nowUtc
            };

            await _invoiceRepository.AddAsync(invoice, cancellationToken);
            var email = new EmailMessageBuilder()
       .To(_currentUser.Email!)
       .Subject("Booking Confirmation & Invoice")
       .HtmlBody($@"
            <h2>Booking Confirmed 🎉</h2>
            <p>Hotel: {hotel.Name}</p>
            <p>Check-in: {request.CheckInDate:yyyy-MM-dd}</p>
            <p>Check-out: {request.CheckOutDate:yyyy-MM-dd}</p>
            <p>Total: {totalPrice} $</p>
            <p>Invoice Number: {invoice.InvoiceNumber}</p>
        ")
       .Build();

            await _emailService.SendAsync(email, cancellationToken);

            return booking.Id;
        }
    }
}