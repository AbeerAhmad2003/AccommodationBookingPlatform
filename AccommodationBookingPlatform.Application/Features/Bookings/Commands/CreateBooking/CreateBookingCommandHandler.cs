using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Contracts.Services.Pricing;
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
            var hotel = await _hotelRepository.GetByIdWithRoomClassesAsync(
                request.HotelId, cancellationToken);

            if (hotel is null)
                throw new NotFoundException("Hotel", request.HotelId);
            var roomClass = hotel.RoomClasses?.FirstOrDefault(rc => rc.Id == request.RoomClassId);

            if (roomClass is null)
                throw new BadRequestException("Selected room class does not belong to this hotel.");

            // Check Availability
            var isAvailable = await _bookingRepository.IsRoomClassAvailableAsync(
          request.RoomClassId,
          request.CheckInDate,
          request.CheckOutDate,
          request.RoomsCount,
          cancellationToken);

            if (!isAvailable)
                throw new BadRequestException(
                    "Selected room class is not available for the selected dates and rooms count.");

            // 3) Pricing (with snapshot)
            var pricing = await _pricingService.CalculateAsync(
                hotel,
                roomClass,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                request.Adults,
                request.Children,
                cancellationToken);


            // 4) Create Booking with snapshot
            var booking = new Booking
            {
                UserId = userId,
                HotelId = request.HotelId,
                RoomClassId = request.RoomClassId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                Adults = request.Adults,
                Children = request.Children,
                PaymentMethod = request.PaymentMethod,

                PricePerNightAtBooking = pricing.OriginalPricePerNight,
                DiscountPercentageApplied = pricing.DiscountPercentage,
                FinalPricePerNight = pricing.FinalPricePerNight,
                Nights = pricing.Nights,
                TotalPrice = pricing.TotalPrice,

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

                TotalAmount = pricing.TotalPrice,
                PaymentMethod = request.PaymentMethod,
                InvoiceNumber = $"INV-{nowUtc:yyyyMMdd}-{booking.Id.ToString()[..8]}",

                PricePerNightBeforeDiscount = pricing.OriginalPricePerNight,
                DiscountPercentageApplied = pricing.DiscountPercentage,
                FinalPricePerNight = pricing.FinalPricePerNight,
                Nights = pricing.Nights,
                RoomsCount = pricing.RoomsCount,

                CreatedAtUtc = nowUtc
            };
            await _invoiceRepository.AddAsync(invoice, cancellationToken);
            //     var email = new EmailMessageBuilder()
            //.To(_currentUser.Email!)
            //.Subject("Booking Confirmation & Invoice")
            //.HtmlBody($@"
            //     <h2>Booking Confirmed 🎉</h2>
            //     <p>Hotel: {hotel.Name}</p>
            //     <p>Check-in: {request.CheckInDate:yyyy-MM-dd}</p>
            //     <p>Check-out: {request.CheckOutDate:yyyy-MM-dd}</p>
            //     <p>Total: {totalPrice} $</p>
            //     <p>Invoice Number: {invoice.InvoiceNumber}</p>
            // ")
            //.Build();

            // await _emailService.SendAsync(email, cancellationToken);

            return booking.Id;
        }
    }
}