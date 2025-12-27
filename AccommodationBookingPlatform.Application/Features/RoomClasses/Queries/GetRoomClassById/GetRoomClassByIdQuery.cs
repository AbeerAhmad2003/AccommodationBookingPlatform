using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassById
{
    public record GetRoomClassByIdQuery(Guid Id)
       : IRequest<RoomClassDto>;
}
