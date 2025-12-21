using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Cities.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Queries.GetCityById
{
    public class GetCityByIdQueryHandler
      : IRequestHandler<GetCityByIdQuery, CityDto>
    {
        private readonly ICityRepository _repo;
        private readonly IMapper _mapper;

        public GetCityByIdQueryHandler(ICityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CityDto> Handle(GetCityByIdQuery request, CancellationToken ct)
        {
            var city = await _repo.GetByIdAsync(request.Id, ct);

            if (city is null)
                throw new NotFoundException(nameof(City), request.Id);

            var dto = _mapper.Map<CityDto>(city);
            dto.HotelsCount = await _repo.GetHotelsCountAsync(city.Id, ct);

            return dto;
        }
    }

}
