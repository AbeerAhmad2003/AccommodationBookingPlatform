using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Rooms.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandler
     : IRequestHandler<UpdateRoomCommand, RoomDto>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(
            IRoomRepository roomRepository,
            IRoomClassRepository roomClassRepository,
            IMapper mapper)
        {
            _roomRepository = roomRepository;
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
        }

        public async Task<RoomDto> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(request.Id, cancellationToken);

            if (room is null)
                throw new NotFoundException(nameof(Room), request.Id);

            var roomClass = await _roomClassRepository
                .GetByIdAsync(request.RoomClassId, cancellationToken);

            if (roomClass is null)
                throw new NotFoundException(nameof(RoomClass), request.RoomClassId);

            // Check duplicate number in same room class
            var exists = await _roomRepository.ExistsNumberInRoomClassForAnotherRoomAsync(
                request.RoomClassId,
                request.Number,
                request.Id,
                cancellationToken);

            if (exists)
                throw new ConflictException("Room number already exists in this room class.");

            room.RoomClassId = request.RoomClassId;
            room.Number = request.Number;
            room.ModifiedAtUtc = DateTime.UtcNow;

            await _roomRepository.UpdateAsync(room, cancellationToken);

            room.RoomClass = roomClass;

            return _mapper.Map<RoomDto>(room);
        }
    }

}
