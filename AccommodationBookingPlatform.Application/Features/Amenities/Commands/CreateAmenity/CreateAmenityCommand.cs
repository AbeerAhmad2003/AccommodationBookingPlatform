using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Commands.CreateAmenity
{
    public record CreateAmenityCommand(
      string Name,
      string? Description
  ) : IRequest<AmenityDto>;

}
