using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUserBookings;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUpcomingBookings
{
    using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
    using AccommodationBookingPlatform.Application.Contracts.Persistence;
    using AutoMapper;
    using MediatR;

    public class GetUpcomingBookingsQueryHandler
        : IRequestHandler<GetUpcomingBookingsQuery, IEnumerable<UserBookingListItemDto>>
    {
        private readonly IBookingRepository _repo;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetUpcomingBookingsQueryHandler(
            IBookingRepository repo,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _repo = repo;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserBookingListItemDto>> Handle(
            GetUpcomingBookingsQuery request,
            CancellationToken cancellationToken)
        {
            if (_currentUser.UserId is null)
                throw new UnauthorizedAccessException("User not logged in.");

            var bookings = await _repo.GetByUserIdAsync(
                _currentUser.UserId.Value,
                cancellationToken);

            var upcoming = bookings
                .Where(b => b.CheckInDate > DateTime.UtcNow)
                .OrderBy(b => b.CheckInDate);

            return _mapper.Map<IEnumerable<UserBookingListItemDto>>(upcoming);
        }
    }
}
