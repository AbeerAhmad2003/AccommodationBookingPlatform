using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.UpdateRoomClass
{
    public class UpdateRoomClassCommandHandler
        : IRequestHandler<UpdateRoomClassCommand, RoomClassDto>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;

        public UpdateRoomClassCommandHandler(
            IRoomClassRepository roomClassRepository,
            IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
        }

        public async Task<RoomClassDto> Handle(
            UpdateRoomClassCommand request,
            CancellationToken cancellationToken)
        {
            var roomClass = await _roomClassRepository
                .GetByIdWithDetailsAsync(request.Id, cancellationToken);

            if (roomClass is null)
                throw new NotFoundException(nameof(RoomClass), request.Id);

            roomClass.Name = request.Name;
            roomClass.Description = request.Description;
            roomClass.AdultsCapacity = request.AdultsCapacity;
            roomClass.ChildrenCapacity = request.ChildrenCapacity;
            roomClass.PricePerNight = request.PricePerNight;
            roomClass.RoomType = request.RoomType;
            roomClass.ModifiedAtUtc = DateTime.UtcNow;

            await _roomClassRepository.UpdateAsync(roomClass, cancellationToken);

            var dto = _mapper.Map<RoomClassDto>(roomClass);
            return dto;
        }
    }
}
