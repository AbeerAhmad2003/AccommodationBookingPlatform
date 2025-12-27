using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Queries.GetTrendingCities
{
    public record GetTrendingCitiesQuery(int Count = 5)
       : IRequest<IReadOnlyList<TrendingCityDto>>;
}
