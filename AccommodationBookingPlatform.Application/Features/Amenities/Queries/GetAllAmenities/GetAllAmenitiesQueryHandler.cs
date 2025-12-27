using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using AutoMapper;
using MediatR;



namespace AccommodationBookingPlatform.Application.Features.Amenities.Queries.GetAllAmenities
{
    public class GetAllAmenitiesQueryHandler
      : IRequestHandler<GetAllAmenitiesQuery, IEnumerable<AmenityDto>>
    {
        private readonly IAmenityRepository _repo;
        private readonly IMapper _mapper;

        public GetAllAmenitiesQueryHandler(IAmenityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AmenityDto>> Handle(GetAllAmenitiesQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<AmenityDto>>(list);
        }
    }

}
