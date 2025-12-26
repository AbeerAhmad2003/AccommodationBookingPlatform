using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.RemoveAmenityFromRoomClass
{
    public record RemoveAmenityFromRoomClassCommand(
     Guid RoomClassId,
     Guid AmenityId
 ) : IRequest;

}
