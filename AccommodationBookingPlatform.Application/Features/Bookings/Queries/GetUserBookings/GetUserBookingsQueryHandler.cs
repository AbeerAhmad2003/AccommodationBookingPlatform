using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUserBookings
{
    public class GetUserBookingsQueryHandler
      : IRequestHandler<GetUserBookingsQuery, IEnumerable<UserBookingListItemDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetUserBookingsQueryHandler(
            IBookingRepository bookingRepository,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserBookingListItemDto>> Handle(
            GetUserBookingsQuery request,
            CancellationToken cancellationToken)
        {
            if (_currentUser.UserId is null)
                throw new UnauthorizedAccessException("User not logged in.");

            var bookings = await _bookingRepository.GetByUserIdAsync(
                _currentUser.UserId.Value,
                cancellationToken);

            return _mapper.Map<IEnumerable<UserBookingListItemDto>>(bookings);
        }
    }

}
