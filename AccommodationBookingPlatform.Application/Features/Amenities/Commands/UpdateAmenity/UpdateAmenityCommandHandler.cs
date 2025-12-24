using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Commands.UpdateAmenity
{
    public class UpdateAmenityCommandHandler
      : IRequestHandler<UpdateAmenityCommand, AmenityDto>
    {
        private readonly IAmenityRepository _repo;
        private readonly IMapper _mapper;

        public UpdateAmenityCommandHandler(IAmenityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AmenityDto> Handle(UpdateAmenityCommand request, CancellationToken ct)
        {
            var amenity = await _repo.GetByIdAsync(request.Id, ct);

            if (amenity is null)
                throw new NotFoundException(nameof(Amenity), request.Id);

            amenity.Name = request.Name;
            amenity.Description = request.Description;
            amenity.ModifiedAtUtc = DateTime.UtcNow;

            await _repo.UpdateAsync(amenity, ct);

            return _mapper.Map<AmenityDto>(amenity);
        }
    }

}
