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
                    (string.IsNullOrWhiteSpace(request.CityName) ||
                        h.City.Name.Contains(request.CityName)) &&
                    (!request.MinStars.HasValue || h.ReviewsRating >= request.MinStars) &&
                    (!request.MaxStars.HasValue || h.ReviewsRating <= request.MaxStars)
            };

            var hotelsPaged = await _hotelRepository.SearchAsync(query, cancellationToken);

            var mappedItems = _mapper.Map<List<HotelSearchResultDto>>(hotelsPaged.Items);

            return new PaginatedList<HotelSearchResultDto>(
                mappedItems,
                hotelsPaged.TotalCount,
                hotelsPaged.PageNumber,
                hotelsPaged.PageSize);
        }
    }

}
