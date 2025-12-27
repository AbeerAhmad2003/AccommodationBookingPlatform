using AccommodationBookingPlatform.Application.Features.Cities.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Commands.CreateCity
{
    public record CreateCityCommand(
     string Name,
     string Country,
     string PostOffice
 ) : IRequest<CityDto>;
}
