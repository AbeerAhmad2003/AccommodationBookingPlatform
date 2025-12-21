using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.CreateRoomClass
{
    public record CreateRoomClassCommand(RoomClassInputDto RoomClass)
         : IRequest<RoomClassDto>;
}
