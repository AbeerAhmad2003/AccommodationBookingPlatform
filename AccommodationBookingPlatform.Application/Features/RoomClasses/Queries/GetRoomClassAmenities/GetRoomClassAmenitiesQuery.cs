using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassAmenities
{
    public record GetRoomClassAmenitiesQuery(
     Guid RoomClassId
 ) : IRequest<IEnumerable<AmenityDto>>;

}
