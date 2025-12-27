using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Features.Cities.Common;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesQueryHandler
     : IRequestHandler<GetCitiesQuery, PaginatedList<CityDto>>
    {
        private readonly ICityRepository _repo;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(ICityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CityDto>> Handle(
            GetCitiesQuery request,
            CancellationToken ct)
        {
            var query = new Query<City>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Filter = string.IsNullOrWhiteSpace(request.Search)
                    ? null
                    : c => c.Name.Contains(request.Search)
                           || c.Country.Contains(request.Search)
            };

            var result = await _repo.GetCitiesAsync(query, ct);

            var mapped = _mapper.Map<List<CityDto>>(result.Items);

            return new PaginatedList<CityDto>(
                mapped,
                result.TotalCount,
                result.PageNumber,
                result.PageSize);
        }
    }

}
