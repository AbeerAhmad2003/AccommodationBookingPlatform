using AccommodationBookingPlatform.Domain.Entities;
using System.Linq.Expressions;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<IEnumerable<Discount>> FindAsync(
    Expression<Func<Discount, bool>> predicate,
    CancellationToken ct);


    }
}
