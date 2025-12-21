using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Cities.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandHandler
      : IRequestHandler<CreateCityCommand, CityDto>
    {
        private readonly ICityRepository _repo;
        private readonly IMapper _mapper;

        public CreateCityCommandHandler(ICityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CityDto> Handle(CreateCityCommand request, CancellationToken ct)
        {
            if (await _repo.ExistsByNameAsync(request.Name, ct))
                throw new ConflictException("City with same name already exists.");

            var city = new City
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Country = request.Country,
                PostOffice = request.PostOffice,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _repo.AddAsync(city, ct);

            var dto = _mapper.Map<CityDto>(city);
            dto.HotelsCount = 0;

            return dto;
        }
    }

}
