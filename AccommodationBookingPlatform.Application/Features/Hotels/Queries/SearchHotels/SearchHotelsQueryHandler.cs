using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.SearchHotels
{
    public class SearchHotelsQueryHandler
     : IRequestHandler<SearchHotelsQuery, PaginatedList<HotelSearchResultDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public SearchHotelsQueryHandler(
            IHotelRepository hotelRepository,
            IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<HotelSearchResultDto>> Handle(
            SearchHotelsQuery request,
            CancellationToken cancellationToken)
        {
            var query = new Query<Hotel>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SortColumn = request.SortColumn,
                SortOrder = request.SortOrder,

                Filter = h =>
                    (request.CityId == null || h.CityId == request.CityId) &&
                    (string.IsNullOrWhiteSpace(request.CityName)
                        || h.City.Name.Contains(request.CityName)) &&
                    (!request.MinStars.HasValue || h.ReviewsRating >= request.MinStars) &&
                    (!request.MaxStars.HasValue || h.ReviewsRating <= request.MaxStars)
            };

            var hotelsPaged = await _hotelRepository.SearchAsync(query, cancellationToken);

            var hotels = hotelsPaged.Items.ToList();

            // 🔥 Availability Check
            if (request.CheckIn.HasValue && request.CheckOut.HasValue && request.Rooms > 0)
            {
                var checkIn = request.CheckIn.Value;
                var checkOut = request.CheckOut.Value;

                if (checkIn < checkOut)
                {
                    hotels = hotels
                        .Where(h =>
                        {
                            var totalRooms = h.RoomClasses.Sum(rc => rc.Rooms.Count);

                            var bookedRooms = h.Bookings
                                .Where(b =>
                                    b.CheckInDate < checkOut &&
                                    b.CheckOutDate > checkIn)
                                .Sum(b => b.RoomsCount);

                            var availableRooms = totalRooms - bookedRooms;

                            return availableRooms >= request.Rooms;
                        })
                        .ToList();
                }
            }

            var mappedItems = _mapper.Map<List<HotelSearchResultDto>>(hotels);

            return new PaginatedList<HotelSearchResultDto>(
                mappedItems,
                mappedItems.Count,
                request.PageNumber,
                request.PageSize);
        }
    }

}
