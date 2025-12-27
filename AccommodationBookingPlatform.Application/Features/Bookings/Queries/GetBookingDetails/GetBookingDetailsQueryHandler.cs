using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Common.Enums;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetBookingDetails
{
    public class GetBookingDetailsQueryHandler
      : IRequestHandler<GetBookingDetailsQuery, BookingDetailsDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetBookingDetailsQueryHandler(
            IBookingRepository bookingRepository,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<BookingDetailsDto> Handle(
            GetBookingDetailsQuery request,
            CancellationToken cancellationToken)
        {
            if (_currentUser.UserId is null)
                throw new UnauthorizedAccessException("User not logged in.");

            var booking = await _bookingRepository
                .GetByIdWithDetailsAsync(request.BookingId, cancellationToken);

            if (booking is null)
                throw new NotFoundException("Booking", request.BookingId);

            var isAdmin = _currentUser.Role == UserRole.Admin;


            if (!isAdmin && booking.UserId != _currentUser.UserId.Value)
                throw new ForbiddenException("You cannot view this booking.");

            return _mapper.Map<BookingDetailsDto>(booking);
        }
    }

}
