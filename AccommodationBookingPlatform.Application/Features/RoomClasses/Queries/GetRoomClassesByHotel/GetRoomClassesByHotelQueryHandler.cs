using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Queries.GetRoomClassesByHotel
{
    public class GetRoomClassesByHotelQueryHandler
         : IRequestHandler<GetRoomClassesByHotelQuery, IReadOnlyList<RoomClassDto>>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetRoomClassesByHotelQueryHandler(
            IRoomClassRepository roomClassRepository,
            IHotelRepository hotelRepository,
            IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<RoomClassDto>> Handle(
            GetRoomClassesByHotelQuery request,
            CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository
                .GetByIdAsync(request.HotelId, cancellationToken);

            if (hotel is null)
                throw new NotFoundException(nameof(Hotel), request.HotelId);

            var roomClasses = await _roomClassRepository
                .GetByHotelIdAsync(request.HotelId, cancellationToken);

            var dtos = _mapper.Map<IReadOnlyList<RoomClassDto>>(roomClasses);
            return dtos;
        }
    }
}
