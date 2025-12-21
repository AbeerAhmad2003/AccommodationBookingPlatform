using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Cities.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandHandler
      : IRequestHandler<UpdateCityCommand, CityDto>
    {
        private readonly ICityRepository _repo;
        private readonly IMapper _mapper;

        public UpdateCityCommandHandler(ICityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CityDto> Handle(UpdateCityCommand request, CancellationToken ct)
        {
            var city = await _repo.GetByIdAsync(request.Id, ct);

            if (city is null)
                throw new NotFoundException(nameof(City), request.Id);

            city.Name = request.Name;
            city.Country = request.Country;
            city.PostOffice = request.PostOffice;
            city.ModifiedAtUtc = DateTime.UtcNow;

            await _repo.UpdateAsync(city, ct);

            var dto = _mapper.Map<CityDto>(city);
            dto.HotelsCount = await _repo.GetHotelsCountAsync(city.Id, ct);

            return dto;
        }
    }

}
