using AccommodationBookingPlatform.Domain.Common.Enums;
using System.Linq.Expressions;

namespace AccommodationBookingPlatform.Domain.Common
{
    public record Query<TEntity>(
    Expression<Func<TEntity, bool>>? Filter = null,
    SortOrder SortOrder = SortOrder.Ascending,
    string? SortColumn = null,
    int PageNumber = 1,
    int PageSize = 10
);
}
