using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUserBookings;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetPastBookings
{
    public class GetPastBookingsQueryHandler
    : IRequestHandler<GetPastBookingsQuery, IEnumerable<UserBookingListItemDto>>
    {
        private readonly IBookingRepository _repo;
        private readonly ICurrentUserService _user;
        private readonly IMapper _mapper;

        public GetPastBookingsQueryHandler(
            IBookingRepository repo,
            ICurrentUserService user,
            IMapper mapper)
        {
            _repo = repo;
            _user = user;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserBookingListItemDto>> Handle(
            GetPastBookingsQuery request,
            CancellationToken ct)
        {
            if (_user.UserId is null)
                throw new UnauthorizedAccessException();

            var bookings = await _repo.GetByUserIdAsync(_user.UserId.Value, ct);

            var result = bookings
                .Where(b => b.CheckOutDate < DateTime.UtcNow)
                .OrderByDescending(b => b.CheckOutDate);

            return _mapper.Map<IEnumerable<UserBookingListItemDto>>(result);
        }
    }

}
