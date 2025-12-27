using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using AccommodationBookingPlatform.Domain.Common.Enums;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.CreateRoomClass
{
    public record CreateRoomClassCommand(
     Guid HotelId,
    string Name,
    string? Description,
    int AdultsCapacity,
    int ChildrenCapacity,
    decimal PricePerNight,
    RoomType RoomType) : IRequest<RoomClassDto>;
}
