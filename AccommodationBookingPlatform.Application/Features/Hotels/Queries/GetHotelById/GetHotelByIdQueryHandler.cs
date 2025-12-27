using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Hotels.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelById
{
    public class GetHotelByIdQueryHandler
        : IRequestHandler<GetHotelByIdQuery, HotelDto>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetHotelByIdQueryHandler(
            IHotelRepository hotelRepository,
            IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<HotelDto> Handle(
            GetHotelByIdQuery request,
            CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (hotel is null)
                throw new NotFoundException(nameof(Hotel), request.Id);

            var dto = _mapper.Map<HotelDto>(hotel);

            dto.RoomsCount = hotel.RoomClasses?.Sum(rc => rc.Rooms.Count) ?? 0;

            return dto;
        }
    }
}
