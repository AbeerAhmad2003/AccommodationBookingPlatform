using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Contracts.Services.Pricing;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Common.Enums;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IBookingPricingService _pricingService;
        private readonly ICurrentUserService _currentUser;

        public UpdateBookingCommandHandler(
            IBookingRepository bookingRepository,
            IHotelRepository hotelRepository,
            IBookingPricingService pricingService,
            ICurrentUserService currentUser)
        {
            _bookingRepository = bookingRepository;
            _hotelRepository = hotelRepository;
            _pricingService = pricingService;
            _currentUser = currentUser;
        }

        public async Task Handle(UpdateBookingCommand request, CancellationToken ct)
        {
            // 1) Auth
            if (_currentUser.UserId is null)
                throw new UnauthorizedAccessException("User not logged in.");

            // 2) Load booking
            var booking = await _bookingRepository.GetByIdWithDetailsAsync(request.BookingId, ct);
            if (booking is null)
                throw new NotFoundException("Booking", request.BookingId);

            var isAdmin = _currentUser.Role == UserRole.Admin;
            var isOwner = booking.UserId == _currentUser.UserId.Value;

            if (!isAdmin && !isOwner)
                throw new ForbiddenException("You cannot modify this booking.");

            // 3) Prevent update after check-in started for normal users
            if (!isAdmin && booking.CheckInDate <= DateTime.UtcNow)
                throw new BadRequestException("You cannot modify a booking after check-in has started.");

            // 4) Validate input
            if (request.CheckInDate >= request.CheckOutDate)
                throw new BadRequestException("Invalid date range. Check-out must be after check-in.");

            if (request.RoomsCount <= 0)
                throw new BadRequestException("Rooms count must be greater than zero.");

            // 5) Determine if allocation needs to change
            var needsReallocation =
                booking.RoomClassId != request.RoomClassId ||
                booking.CheckInDate != request.CheckInDate ||
                booking.CheckOutDate != request.CheckOutDate ||
                booking.RoomsCount != request.RoomsCount;

            // 6) Always allowed fields
            booking.Adults = request.Adults;
            booking.Children = request.Children;
            booking.PaymentMethod = request.PaymentMethod;

            // 7) If no reallocation needed, just save
            if (!needsReallocation)
            {
                booking.ModifiedAtUtc = DateTime.UtcNow;
                await _bookingRepository.UpdateAsync(booking, ct);
                return;
            }

            // 8) Load hotel with room classes to validate new room class
            var hotel = await _hotelRepository.GetByIdWithRoomClassesAsync(booking.HotelId, ct);
            if (hotel is null)
                throw new NotFoundException("Hotel", booking.HotelId);

            var roomClass = hotel.RoomClasses?.FirstOrDefault(rc => rc.Id == request.RoomClassId);
            if (roomClass is null)
                throw new BadRequestException("Selected room class does not belong to this hotel.");

            // 9) Re-allocation steps:
            // 9.1 Free old allocated rooms
            await _bookingRepository.RemoveBookingRoomsAsync(booking.Id, ct);

            // 9.2 Allocate new rooms
            var allocatedRooms = await _bookingRepository.AllocateRoomsAsync(
                request.RoomClassId,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                ct);

            if (allocatedRooms.Count != request.RoomsCount)
                throw new BadRequestException("Not enough rooms available for the selected dates and rooms count.");

            // 9.3 Apply core changes
            booking.RoomClassId = request.RoomClassId;
            booking.CheckInDate = request.CheckInDate;
            booking.CheckOutDate = request.CheckOutDate;
            booking.SetRoomsCount(request.RoomsCount);

            // 10) Recalculate pricing snapshot
            var pricing = await _pricingService.CalculateAsync(
                hotel,
                roomClass,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                request.Adults,
                request.Children,
                ct);

            booking.PricePerNightAtBooking = pricing.OriginalPricePerNight;
            booking.DiscountPercentageApplied = pricing.DiscountPercentage;
            booking.FinalPricePerNight = pricing.FinalPricePerNight;
            booking.Nights = pricing.Nights;
            booking.TotalPrice = pricing.TotalPrice;

            booking.ModifiedAtUtc = DateTime.UtcNow;

            // 11) Save booking changes
            await _bookingRepository.UpdateAsync(booking, ct);

            // 12) Save new allocated rooms
            await _bookingRepository.AddBookingRoomsAsync(
                booking.Id,
                allocatedRooms.Select(r => r.Id).ToList(),
                ct);
        }
    }
}
