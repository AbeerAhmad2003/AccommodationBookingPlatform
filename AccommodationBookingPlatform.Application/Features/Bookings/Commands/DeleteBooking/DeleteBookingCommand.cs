using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.DeleteBooking
{
    public record DeleteBookingCommand(Guid BookingId) : IRequest;
}
