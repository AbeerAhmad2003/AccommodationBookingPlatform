using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Queries.GetAmenityById
{
    public record GetAmenityByIdQuery(Guid Id) : IRequest<AmenityDto>;

}
