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
            var email = new EmailMessageBuilder()
       .To(_currentUser.Email!)
       .Subject("Booking Confirmation & Invoice")
       .HtmlBody($@"
        <div style='font-family:Arial; padding:15px'>
            <h2 style='color:#2c3e50;'>Booking Confirmed 🎉</h2>

            <p>Dear Customer,</p>
            <p>Your booking has been successfully confirmed. Below are your booking details:</p>

            <hr/>

            <h3>🏨 Hotel Information</h3>
            <p>
                <b>Hotel:</b> {hotel.Name}<br/>
                <b>Room Class:</b> {roomClass.Name}<br/>
                <b>Check-in:</b> {request.CheckInDate:yyyy-MM-dd}<br/>
                <b>Check-out:</b> {request.CheckOutDate:yyyy-MM-dd}<br/>
                <b>Nights:</b> {pricing.Nights}<br/>
                <b>Rooms:</b> {pricing.RoomsCount}
            </p>

            <hr/>

            <h3>💰 Pricing Summary</h3>
            <p>
                <b>Original Price / Night:</b> {pricing.OriginalPricePerNight} $<br/>
                <b>Discount Applied:</b> {(pricing.DiscountPercentage is null ? "No Discount" : pricing.DiscountPercentage + "%")}<br/>
                <b>Final Price / Night:</b> {pricing.FinalPricePerNight} $<br/>
                <b>Total:</b> <span style='color:green;font-size:18px;'>{pricing.TotalPrice} $</span>
            </p>

            <hr/>

            <h3>📄 Invoice</h3>
            <p>
                <b>Invoice Number:</b> {invoice.InvoiceNumber}<br/>
                <b>Created:</b> {invoice.CreatedAtUtc}
            </p>

            <p>You can download your invoice from your dashboard.</p>

            <br/>

            <p>Thank you for choosing us 💙</p>

            <hr/>
            <p style='font-size:12px;color:#888'>
                Accommodation Booking System
            </p>
        </div>
    ")
       .Build();

            await _emailService.SendAsync(email, cancellationToken);


            return booking.Id;
        }
    }
}