using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Queries.GetAllAmenities
{
    public record GetAllAmenitiesQuery : IRequest<IEnumerable<AmenityDto>>;

}
