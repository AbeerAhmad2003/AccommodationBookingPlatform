using AccommodationBookingPlatform.Application.Features.Cities.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Queries.GetCityById
{
    public record GetCityByIdQuery(Guid Id) : IRequest<CityDto>;
}
