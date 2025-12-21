using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Hotels.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommandHandler
        : IRequestHandler<UpdateHotelCommand, HotelDto>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public UpdateHotelCommandHandler(
            IHotelRepository hotelRepository,
            ICityRepository cityRepository,
            IOwnerRepository ownerRepository,
            IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _cityRepository = cityRepository;
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<HotelDto> Handle(
            UpdateHotelCommand request,
            CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (hotel is null)
                throw new NotFoundException(nameof(Hotel), request.Id);

            var city = await _cityRepository
                .GetByIdAsync(request.CityId, cancellationToken);

            if (city is null)
                throw new NotFoundException(nameof(City), request.CityId);

            var owner = await _ownerRepository
                .GetByIdAsync(request.OwnerId, cancellationToken);

            if (owner is null)
                throw new NotFoundException(nameof(Owner), request.OwnerId);

            var locationConflict = await _hotelRepository
                .ExistsAtLocationAsync(
                    request.CityId,
                    request.Longitude,
                    request.Latitude,
                    ct: cancellationToken);

            if (locationConflict &&
                (hotel.Longitude != request.Longitude ||
                 hotel.Latitude != request.Latitude ||
                 hotel.CityId != request.CityId))
            {
                throw new ConflictException("Another hotel already exists at this location.");
            }

            hotel.Name = request.Name;
            hotel.CityId = request.CityId;
            hotel.OwnerId = request.OwnerId;
            hotel.PhoneNumber = request.PhoneNumber;
            hotel.Longitude = request.Longitude;
            hotel.Latitude = request.Latitude;
            hotel.BriefDescription = request.BriefDescription;
            hotel.Description = request.Description;
            hotel.ModifiedAtUtc = DateTime.UtcNow;

            await _hotelRepository.UpdateAsync(hotel, cancellationToken);

            var dto = _mapper.Map<HotelDto>(hotel);
            dto.CityName = city.Name;
            dto.OwnerName = $"{owner.FirstName} {owner.LastName}";
            dto.RoomsCount = hotel.RoomClasses?.Sum(r => r.Rooms.Count) ?? 0;

            return dto;
        }
    }
}
