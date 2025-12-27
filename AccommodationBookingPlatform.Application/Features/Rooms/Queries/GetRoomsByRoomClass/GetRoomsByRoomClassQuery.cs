using AccommodationBookingPlatform.Application.Features.Rooms.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Rooms.Queries.GetRoomsByRoomClass
{
    public record GetRoomsByRoomClassQuery(Guid RoomClassId)
    : IRequest<IReadOnlyList<RoomDto>>;

}
