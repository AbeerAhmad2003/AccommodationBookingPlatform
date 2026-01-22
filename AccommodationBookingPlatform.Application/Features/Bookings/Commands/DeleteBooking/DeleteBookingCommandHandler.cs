using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Domain.Common.Enums;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUser;

        public DeleteBookingCommandHandler(
            IBookingRepository bookingRepository,
            ICurrentUserService currentUser)
        {
            _bookingRepository = bookingRepository;
            _currentUser = currentUser;
        }

        public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserId is null)
                throw new UnauthorizedAccessException("User not logged in.");

            var booking = await _bookingRepository.GetByIdWithDetailsAsync(request.BookingId, cancellationToken);
            if (booking is null)
                throw new NotFoundException("Booking", request.BookingId);

            var isAdmin = _currentUser.Role == UserRole.Admin;
            var isOwner = booking.UserId == _currentUser.UserId.Value;

            // Admin can delete any booking, user can delete only own booking
            if (!isAdmin && !isOwner)
                throw new ForbiddenException("You cannot delete this booking.");

            // Prevent user cancel after stay started (admin allowed anytime)
            if (!isAdmin && booking.CheckInDate <= DateTime.UtcNow)
                throw new BadRequestException("You cannot cancel this booking after check-in time has started.");

            await _bookingRepository.DeleteAsync(request.BookingId, cancellationToken);
        }
    }
}
