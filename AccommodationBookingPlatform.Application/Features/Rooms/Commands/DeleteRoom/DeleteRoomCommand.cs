using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Rooms.Commands.DeleteRoom
{
    public record DeleteRoomCommand(Guid Id) : IRequest;

}
