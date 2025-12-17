using AccommodationBookingPlatform.Domain.Entities;
using System.Linq.Expressions;

namespace AccommodationBookingPlatform.Persistence.Sorting
{
    public static class HotelSortingExpressions
    {
        public static Expression<Func<Hotel, object>> Get(
            string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "id" => h => h.Id,
                "name" => h => h.Name,
                "reviewsrating" => h => h.ReviewsRating,
                "createdat" => h => h.CreatedAtUtc,
                _ => h => h.Id
            };
        }
    }
}
