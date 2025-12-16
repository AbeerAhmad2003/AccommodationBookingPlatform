using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Queries.GetTrendingCities
{


    public class GetTrendingCitiesQueryHandler : IRequestHandler<GetTrendingCitiesQuery, IReadOnlyList<TrendingCityDto>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public GetTrendingCitiesQueryHandler(
            ICityRepository cityRepository,
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TrendingCityDto>> Handle(
            GetTrendingCitiesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _cityRepository
                .GetTrendingAsync(request.Count, cancellationToken);

            return result.Select(x =>
            {
                var dto = _mapper.Map<TrendingCityDto>(x.City);
                dto.BookingsCount = x.BookingsCount;
                return dto;
            }).ToList();
        }
    }

}
