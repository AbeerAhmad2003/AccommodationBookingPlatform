using AccommodationBookingPlatform.Application.Features.Cities.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Commands.UpdateCity
{
    public record UpdateCityCommand(
    Guid Id,
    string Name,
    string Country,
    string PostOffice
) : IRequest<CityDto>;
}
