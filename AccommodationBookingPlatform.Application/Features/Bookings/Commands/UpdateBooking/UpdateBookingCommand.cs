using AccommodationBookingPlatform.Domain.Common.Enums;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.UpdateBooking
{
    public record UpdateBookingCommand(
    DateTime CheckInDate,
    DateTime CheckOutDate,
    int Adults,
    int Children,
    int RoomsCount,
    PaymentMethod PaymentMethod
) : IRequest
    {
        public Guid BookingId { get; init; }
    }
}
