using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Contracts.Services.Pricing;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Entities;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
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

        public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // 1) Auth + basic validations
            if (_currentUser.UserId is null)
                throw new UnauthorizedAccessException("User not logged in.");

            var userId = _currentUser.UserId.Value;

            var userEmail = _currentUser.Email;
            if (string.IsNullOrWhiteSpace(userEmail))
                throw new BadRequestException("User email is missing.");

            if (request.RoomsCount <= 0)
                throw new BadRequestException("Rooms count must be greater than zero.");

            if (request.CheckInDate >= request.CheckOutDate)
                throw new BadRequestException("Invalid date range. Check-out must be after check-in.");

            // 2) Load hotel + validate room class belongs to the hotel
            var hotel = await _hotelRepository.GetByIdWithRoomClassesAsync(request.HotelId, cancellationToken);
            if (hotel is null)
                throw new NotFoundException("Hotel", request.HotelId);

            var roomClass = hotel.RoomClasses?.FirstOrDefault(rc => rc.Id == request.RoomClassId);
            if (roomClass is null)
                throw new BadRequestException("Selected room class does not belong to this hotel.");

            // 3) Allocate actual rooms (source of truth for availability)
            var allocatedRooms = await _bookingRepository.AllocateRoomsAsync(
                request.RoomClassId,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                cancellationToken);

            if (allocatedRooms.Count != request.RoomsCount)
                throw new BadRequestException("Selected room class is not available for the selected dates and rooms count.");

            // 4) Pricing snapshot
            var pricing = await _pricingService.CalculateAsync(
                hotel,
                roomClass,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                request.Adults,
                request.Children,
                cancellationToken);

            // 5) Create booking
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

            // 6) Persist allocated rooms (BookingRooms junction table)
            await _bookingRepository.AddBookingRoomsAsync(
                booking.Id,
                allocatedRooms.Select(r => r.Id).ToList(),
                cancellationToken);

            // 7) Create invoice
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

            // 8) Send confirmation email (formatting handled inside EmailService)
            await _emailService.SendBookingConfirmationAsync(
                userEmail,
                hotel.Name,
                roomClass.Name,
                request.CheckInDate,
                request.CheckOutDate,
                pricing.Nights,
                pricing.RoomsCount,
                allocatedRooms.Select(r => r.Number),
                pricing.OriginalPricePerNight,
                pricing.DiscountPercentage,
                pricing.FinalPricePerNight,
                pricing.TotalPrice,
                invoice.InvoiceNumber,
                invoice.CreatedAtUtc,
                cancellationToken);

            return booking.Id;
        }
    }
}