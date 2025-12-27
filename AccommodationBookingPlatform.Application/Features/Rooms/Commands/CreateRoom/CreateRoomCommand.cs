using AccommodationBookingPlatform.Application.Features.Rooms.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(
      Guid RoomClassId,
      string Number
  ) : IRequest<RoomDto>;

}
