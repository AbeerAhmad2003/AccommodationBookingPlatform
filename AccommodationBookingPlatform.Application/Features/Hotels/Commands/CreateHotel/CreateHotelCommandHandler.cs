using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Hotels.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandHandler
      : IRequestHandler<CreateHotelCommand, HotelDto>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public CreateHotelCommandHandler(
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
            CreateHotelCommand request,
            CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(request.CityId, cancellationToken);
            if (city is null)
                throw new NotFoundException(nameof(City), request.CityId);

            var owner = await _ownerRepository.GetByIdAsync(request.OwnerId, cancellationToken);
            if (owner is null)
                throw new NotFoundException(nameof(Owner), request.OwnerId);

            var existsAtSameLocation = await _hotelRepository
                .ExistsAtLocationAsync(
                    request.CityId,
                    request.Longitude,
                    request.Latitude,
                    ct: cancellationToken);

            if (existsAtSameLocation)
                throw new ConflictException("A hotel already exists at this location.");

            var hotel = new Hotel
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CityId = request.CityId,
                OwnerId = request.OwnerId,
                PhoneNumber = request.PhoneNumber,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                BriefDescription = request.BriefDescription,
                Description = request.Description,
                ReviewsRating = 0,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _hotelRepository.AddAsync(hotel, cancellationToken);

            var dto = _mapper.Map<HotelDto>(hotel);
            dto.CityName = city.Name;
            dto.OwnerName = $"{owner.FirstName} {owner.LastName}";
            dto.RoomsCount = 0;
            dto.ReviewsCount = 0;

            return dto;
        }
    }

}
