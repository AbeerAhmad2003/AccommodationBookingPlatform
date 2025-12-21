using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.DeleteRoomClass
{
    public record DeleteRoomClassCommand(Guid Id) : IRequest;
}
