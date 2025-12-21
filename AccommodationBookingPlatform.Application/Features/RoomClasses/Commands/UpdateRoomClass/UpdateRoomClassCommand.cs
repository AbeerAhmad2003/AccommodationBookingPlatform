using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using AccommodationBookingPlatform.Domain.Common.Enums;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.UpdateRoomClass
{
    public record UpdateRoomClassCommand(
         Guid Id,
         string Name,
         string? Description,
         int AdultsCapacity,
         int ChildrenCapacity,
         decimal PricePerNight,
         RoomType RoomType
     ) : IRequest<RoomClassDto>;
}
