using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Features.Hotels.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotels
{
    public record GetHotelsQuery(
     int PageNumber = 1,
     int PageSize = 10,
     string? Search = null
 ) : IRequest<PaginatedList<HotelDto>>;
}
