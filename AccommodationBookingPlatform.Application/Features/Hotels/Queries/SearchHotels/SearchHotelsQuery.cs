using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Domain.Common.Enums;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.SearchHotels
{
    public record SearchHotelsQuery(
     Guid? CityId,
     string? CityName,
     double? MinStars,
     double? MaxStars,

     DateTime? CheckIn,
     DateTime? CheckOut,
     int Rooms = 1,

     int PageNumber = 1,
     int PageSize = 10,
     string? SortColumn = "Rating",
     SortOrder SortOrder = SortOrder.Desc,

     List<Guid>? AmenityIds = null,

     decimal? MinPrice = null,
     decimal? MaxPrice = null,

     RoomType? RoomType = null
 ) : IRequest<PaginatedList<HotelSearchResultDto>>;
}