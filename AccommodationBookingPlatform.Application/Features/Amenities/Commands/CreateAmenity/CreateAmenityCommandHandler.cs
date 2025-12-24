using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Commands.CreateAmenity
{
    public class CreateAmenityCommandHandler
     : IRequestHandler<CreateAmenityCommand, AmenityDto>
    {
        private readonly IAmenityRepository _repo;
        private readonly IMapper _mapper;

        public CreateAmenityCommandHandler(IAmenityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AmenityDto> Handle(CreateAmenityCommand request, CancellationToken ct)
        {
            var amenity = new Amenity
            {
                Name = request.Name,
                Description = request.Description
            };

            await _repo.AddAsync(amenity, ct);

            return _mapper.Map<AmenityDto>(amenity);
        }
    }

}
