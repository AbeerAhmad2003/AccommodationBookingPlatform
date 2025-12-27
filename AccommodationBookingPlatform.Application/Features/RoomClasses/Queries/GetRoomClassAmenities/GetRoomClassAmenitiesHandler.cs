using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Features.Amenities.Common;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassAmenities
{
    public class GetRoomClassAmenitiesHandler
      : IRequestHandler<GetRoomClassAmenitiesQuery, IEnumerable<AmenityDto>>
    {
        private readonly IAmenityRepository _amenityRepo;
        private readonly IMapper _mapper;

        public GetRoomClassAmenitiesHandler(
            IAmenityRepository amenityRepo,
            IMapper mapper)
        {
            _amenityRepo = amenityRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AmenityDto>> Handle(
            GetRoomClassAmenitiesQuery request,
            CancellationToken ct)
        {
            var list = await _amenityRepo.GetByRoomClassIdAsync(request.RoomClassId, ct);
            return _mapper.Map<IEnumerable<AmenityDto>>(list);
        }
    }

}
