using AccommodationBookingPlatform.Domain.Entities;
using System.Linq.Expressions;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface ICityRepository : IRepository<City>
    {
        Task<bool> HaveHotels(Expression<Func<City, bool>> predicate, CancellationToken cancellationToken);
        Task<bool> HaveUsers(Expression<Func<City, bool>> predicate, CancellationToken cancellationToken);
        Task<IReadOnlyList<(City City, int BookingsCount)>> GetTrendingAsync(int count, CancellationToken cancellationToken = default);
    }
}
