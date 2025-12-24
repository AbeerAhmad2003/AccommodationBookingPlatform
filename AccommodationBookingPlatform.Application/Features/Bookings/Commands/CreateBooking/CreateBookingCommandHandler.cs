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

        public CreateBookingCommandHandler(
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

        public async Task<Guid> Handle(
            CreateBookingCommand request,
            CancellationToken cancellationToken)
        {
            if (_currentUser.UserId is null)
                throw new UnauthorizedAccessException("User not logged in.");

            var userId = _currentUser.UserId.Value;

            // 1️⃣ Check hotel exists
            var hotel = await _hotelRepository.GetByIdAsync(
                request.HotelId, cancellationToken);

            if (hotel is null)
                throw new NotFoundException("Hotel", request.HotelId);

            // 2️⃣ Availability
            var isAvailable = await _bookingRepository.IsHotelAvailableAsync(
                request.HotelId,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                cancellationToken);

            if (!isAvailable)
                throw new BadRequestException(
               "Hotel is not available for the selected dates and rooms count.");


            // 3️⃣ Pricing
            var totalPrice = await _pricingService.CalculateTotalPriceAsync(
                hotel,
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomsCount,
                request.Adults,
                request.Children,
                cancellationToken);

            // 4️⃣ Create Booking
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

            return booking.Id;
        }
    }
}
