using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Commands.UpdateAmenity
{
    public record UpdateAmenityCommand(
     Guid Id,
     string Name,
     string? Description
 ) : IRequest<AmenityDto>;

}
