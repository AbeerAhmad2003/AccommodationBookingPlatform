using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Rooms.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Rooms.Queries.GetRoomsByRoomClass
{
    public class GetRoomsByRoomClassQueryHandler
     : IRequestHandler<GetRoomsByRoomClassQuery, IReadOnlyList<RoomDto>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;

        public GetRoomsByRoomClassQueryHandler(
            IRoomRepository roomRepository,
            IRoomClassRepository roomClassRepository,
            IMapper mapper)
        {
            _roomRepository = roomRepository;
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<RoomDto>> Handle(
            GetRoomsByRoomClassQuery request,
            CancellationToken cancellationToken)
        {
            var roomClass = await _roomClassRepository
                .GetByIdAsync(request.RoomClassId, cancellationToken);

            if (roomClass is null)
                throw new NotFoundException(nameof(RoomClass), request.RoomClassId);

            var rooms = await _roomRepository
                .GetByRoomClassIdAsync(request.RoomClassId, cancellationToken);

            return _mapper.Map<IReadOnlyList<RoomDto>>(rooms);
        }
    }

}
