using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelDetails
{
    public class GetHotelDetailsQueryHandler
        : IRequestHandler<GetHotelDetailsQuery, HotelDetailsDto>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetHotelDetailsQueryHandler(
            IHotelRepository hotelRepository,
            IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<HotelDetailsDto> Handle(
            GetHotelDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository
                .GetHotelDetailsAsync(request.HotelId, cancellationToken);

            if (hotel is null)
                throw new NotFoundException(nameof(Hotel), request.HotelId);

            var dto = _mapper.Map<HotelDetailsDto>(hotel);

            // لو ما في تواريخ → رجّع بدون Availability
            if (!request.CheckIn.HasValue || !request.CheckOut.HasValue)
                return dto;

            var checkIn = request.CheckIn.Value;
            var checkOut = request.CheckOut.Value;

            if (checkIn >= checkOut)
                return dto;

            foreach (var roomClass in dto.RoomClasses)
            {
                var domainRoomClass = hotel.RoomClasses.First(rc => rc.Id == roomClass.Id);

                roomClass.TotalRooms = domainRoomClass.Rooms.Count;

                roomClass.BookedRooms = hotel.Bookings
                    .Where(b =>
                        b.CheckInDate < checkOut &&
                        b.CheckOutDate > checkIn)
                    .Sum(b => b.RoomsCount);

                roomClass.IsAvailable = roomClass.TotalRooms - roomClass.BookedRooms >= request.Rooms;
            }

            return dto;
        }
    }
}
