using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetRecentlyVisitedHotel
{
    public class GetRecentlyVisitedHotelsQueryHandler
       : IRequestHandler<GetRecentlyVisitedHotelsQuery, IEnumerable<RecentlyVisitedHotelDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetRecentlyVisitedHotelsQueryHandler(
            IBookingRepository bookingRepository,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecentlyVisitedHotelDto>> Handle(
            GetRecentlyVisitedHotelsQuery request,
            CancellationToken cancellationToken)
        {
            if (_currentUser.UserId is null)
                throw new UnauthorizedAccessException("User not logged in.");

            var bookings = await _bookingRepository
                .GetRecentBookingsInDifferentHotelsByUserId(
                    _currentUser.UserId.Value,
                    request.Count,
                    cancellationToken);

            return _mapper.Map<IEnumerable<RecentlyVisitedHotelDto>>(bookings);
        }
    }

}
