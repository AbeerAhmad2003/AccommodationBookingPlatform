using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Contracts.Services.Pricing;
using AccommodationBookingPlatform.Application.Exceptions;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandHandler
         : IRequestHandler<UpdateBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingPricingService _pricingService;
        private readonly ICurrentUserService _currentUser;

        public UpdateBookingCommandHandler(
            IBookingRepository bookingRepository,
            IBookingPricingService pricingService,
            ICurrentUserService currentUser)
        {
            _bookingRepository = bookingRepository;
            _pricingService = pricingService;
            _currentUser = currentUser;
        }

        public async Task Handle(
            UpdateBookingCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUser.UserId is null)
                throw new UnauthorizedAccessException("User not logged in.");

            var booking = await _bookingRepository.GetByIdWithDetailsAsync(
                request.BookingId, cancellationToken);

            if (booking is null)
                throw new NotFoundException("Booking", request.BookingId);

            // 🔐 Security: allow only owner
            if (booking.UserId != _currentUser.UserId.Value)
                throw new ForbiddenException("You cannot modify this booking.");

            // ⛔ Prevent editing after stay started
            if (booking.CheckInDate <= DateTime.UtcNow)
                throw new BadRequestException(
                    "You cannot modify a booking after check-in has started.");

            // 🏨 Re-check availability
            var isAvailable = await _bookingRepository.IsHotelAvailableAsync(
                booking.HotelId,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                cancellationToken);

            if (!isAvailable)
                throw new BadRequestException(
                    "Rooms are not available for the selected period.");

            // 💰 Recalculate pricing (supports discounts)
            var newPrice = await _pricingService.CalculateTotalPriceAsync(
                booking.Hotel,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                request.Adults,
                request.Children,
                cancellationToken);

            // ✏️ Apply update
            booking.CheckInDate = request.CheckInDate;
            booking.CheckOutDate = request.CheckOutDate;
            booking.Adults = request.Adults;
            booking.Children = request.Children;
            booking.PaymentMethod = request.PaymentMethod;
            booking.TotalPrice = newPrice;
            booking.SetRoomsCount(request.RoomsCount);
            booking.ModifiedAtUtc = DateTime.UtcNow;

            await _bookingRepository.UpdateAsync(booking, cancellationToken);
        }
    }
}
