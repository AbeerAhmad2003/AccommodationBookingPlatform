using AccommodationBookingPlatform.Application.Features.Rooms.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Rooms.Commands.UpdateRoom
{
    public record UpdateRoomCommand(
    Guid RoomClassId,
    string Number
) : IRequest<RoomDto>
    {
        public Guid Id { get; init; }
    }

}
