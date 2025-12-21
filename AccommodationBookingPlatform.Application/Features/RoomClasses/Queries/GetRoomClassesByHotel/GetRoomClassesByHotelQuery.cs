using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassesByHotel
{
    public record GetRoomClassesByHotelQuery(Guid HotelId)
         : IRequest<IReadOnlyList<RoomClassDto>>;
}
