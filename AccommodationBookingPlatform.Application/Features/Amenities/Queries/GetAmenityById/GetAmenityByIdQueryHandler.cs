using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Queries.GetAmenityById
{
    public class GetAmenityByIdQueryHandler
      : IRequestHandler<GetAmenityByIdQuery, AmenityDto>
    {
        private readonly IAmenityRepository _repo;
        private readonly IMapper _mapper;

        public GetAmenityByIdQueryHandler(IAmenityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AmenityDto> Handle(GetAmenityByIdQuery request, CancellationToken ct)
        {
            var amenity = await _repo.GetByIdAsync(request.Id, ct);

            if (amenity is null)
                throw new NotFoundException(nameof(Amenity), request.Id);

            return _mapper.Map<AmenityDto>(amenity);
        }
    }

}
