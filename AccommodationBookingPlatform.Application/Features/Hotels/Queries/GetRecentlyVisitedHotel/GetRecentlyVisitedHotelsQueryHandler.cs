using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetRecentlyVisitedHotel
{
    public class GetRecentlyVisitedHotelsQueryHandler
    : IRequestHandler<GetRecentlyVisitedHotelsQuery, IEnumerable<RecentlyVisitedHotelDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetRecentlyVisitedHotelsQueryHandler(
            IUserRepository userRepository,
            IBookingRepository bookingRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecentlyVisitedHotelDto>> Handle(
            GetRecentlyVisitedHotelsQuery request,
            CancellationToken cancellationToken)
        {
            // 1️⃣ نتأكد إن اليوزر موجود
            var exists = await _userRepository.ExistsByIdAsync(request.UserId, cancellationToken);
            if (!exists)
            {
                // استخدمي الـ NotFoundException اللي عندك في المشروع
                throw new NotFoundException("User not found.");
            }

            // 2️⃣ نجيب آخر الحجوزات في فنادق مختلفة
            var bookings = await _bookingRepository
                .GetRecentBookingsInDifferentHotelsByUserId(
                    request.UserId,
                    request.Count,
                    cancellationToken);

            // 3️⃣ نحول Bookings → DTO
            return _mapper.Map<IEnumerable<RecentlyVisitedHotelDto>>(bookings);
        }
    }

}
