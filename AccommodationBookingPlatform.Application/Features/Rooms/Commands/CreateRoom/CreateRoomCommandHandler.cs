using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom;
using AccommodationBookingPlatform.Application.Features.Rooms.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

public class CreateRoomCommandHandler
       : IRequestHandler<CreateRoomCommand, RoomDto>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomClassRepository _roomClassRepository;
    private readonly IMapper _mapper;

    public CreateRoomCommandHandler(
        IRoomRepository roomRepository,
        IRoomClassRepository roomClassRepository,
        IMapper mapper)
    {
        _roomRepository = roomRepository;
        _roomClassRepository = roomClassRepository;
        _mapper = mapper;
    }

    public async Task<RoomDto> Handle(
        CreateRoomCommand request,
        CancellationToken cancellationToken)
    {
        var roomClass = await _roomClassRepository
            .GetByIdAsync(request.RoomClassId, cancellationToken);

        if (roomClass is null)
            throw new NotFoundException(nameof(RoomClass), request.RoomClassId);

        var exists = await _roomRepository
            .ExistsNumberInRoomClassAsync(
                request.RoomClassId,
                request.Number,
                cancellationToken);

        if (exists)
            throw new ConflictException(
                "Room number already exists in this room class.");

        var entity = new Room
        {
            Id = Guid.NewGuid(),
            RoomClassId = request.RoomClassId,
            Number = request.Number,
            CreatedAtUtc = DateTime.UtcNow
        };

        await _roomRepository.AddAsync(entity, cancellationToken);

        entity.RoomClass = roomClass;

        return _mapper.Map<RoomDto>(entity);
    }
}
