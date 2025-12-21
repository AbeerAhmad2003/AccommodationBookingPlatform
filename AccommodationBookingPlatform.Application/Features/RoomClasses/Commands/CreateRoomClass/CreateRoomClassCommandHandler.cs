using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.CreateRoomClass
{
    public class CreateRoomClassCommandHandler
       : IRequestHandler<CreateRoomClassCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public CreateRoomClassCommandHandler(
            IRoomClassRepository roomClassRepository,
            IHotelRepository hotelRepository,
            IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<RoomClassDto> Handle(
            CreateRoomClassCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.RoomClass;

            var hotel = await _hotelRepository
                .GetByIdAsync(dto.HotelId, cancellationToken);

            if (hotel is null)
                throw new NotFoundException(nameof(Hotel), dto.HotelId);

            var exists = await _roomClassRepository
                .ExistsByNameInHotelAsync(dto.HotelId, dto.Name, cancellationToken);

            if (exists)
                throw new ConflictException("Room class with the same name already exists in this hotel.");

            var entity = new RoomClass
            {
                Id = Guid.NewGuid(),
                HotelId = dto.HotelId,
                Name = dto.Name,
                Description = dto.Description,
                AdultsCapacity = dto.AdultsCapacity,
                ChildrenCapacity = dto.ChildrenCapacity,
                PricePerNight = dto.PricePerNight,
                RoomType = dto.RoomType,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _roomClassRepository.AddAsync(entity, cancellationToken);

            // load hotel name + rooms count
            entity.Hotel = hotel;
            entity.Rooms = new List<Room>();

            var result = _mapper.Map<RoomClassDto>(entity);
            return result;
        }
    }
}
