using AccommodationBookingPlatform.Domain.Common.Enums;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.CreateBooking
{
    public record CreateBookingCommand(
    Guid HotelId,
    Guid RoomClassId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    int Adults,
    int Children,
    int RoomsCount,
    PaymentMethod PaymentMethod
) : IRequest<Guid>;
}