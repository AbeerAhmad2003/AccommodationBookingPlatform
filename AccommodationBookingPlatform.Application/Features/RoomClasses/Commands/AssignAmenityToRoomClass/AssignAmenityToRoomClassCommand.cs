using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.AssignAmenityToRoomClass
{
    public record AssignAmenityToRoomClassCommand(
      Guid RoomClassId,
      Guid AmenityId
  ) : IRequest;

}
