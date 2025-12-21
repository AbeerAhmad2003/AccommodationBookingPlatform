using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Features.Hotels.Common;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotels
{
    public class GetHotelsQueryHandler
         : IRequestHandler<GetHotelsQuery, PaginatedList<HotelDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetHotelsQueryHandler(
            IHotelRepository hotelRepository,
            IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<HotelDto>> Handle(
            GetHotelsQuery request,
            CancellationToken cancellationToken)
        {
            var query = new Query<Hotel>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SortColumn = "CreatedAtUtc",
                SortOrder = Domain.Common.Enums.SortOrder.Desc,

                Filter = h =>
                    string.IsNullOrWhiteSpace(request.Search)
                    || h.Name.Contains(request.Search)
                    || h.City.Name.Contains(request.Search)
            };

            var hotelsPaged = await _hotelRepository
                .GetHotelsAsync(query, cancellationToken);

            var mapped = _mapper.Map<List<HotelDto>>(hotelsPaged.Items);

            return new PaginatedList<HotelDto>(
                mapped,
                hotelsPaged.TotalCount,
                request.PageNumber,
                request.PageSize);
        }
    }
}
