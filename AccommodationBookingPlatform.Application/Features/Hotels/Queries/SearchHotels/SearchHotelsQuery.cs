using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Domain.Common.Enums;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.SearchHotels
{
    public record SearchHotelsQuery(
    Guid? CityId,
    string? CityName,
    DateTime? CheckIn,
    DateTime? CheckOut,
    int Adults = 2,
    int Children = 0,
    int Rooms = 1,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    double? MinStars = null,
    double? MaxStars = null,
    List<string>? Amenities = null,
    int PageNumber = 1,
    int PageSize = 10,
    string? SortColumn = "Rating",
    SortOrder SortOrder = SortOrder.Desc
) : IRequest<PaginatedList<HotelSearchResultDto>>;
}
