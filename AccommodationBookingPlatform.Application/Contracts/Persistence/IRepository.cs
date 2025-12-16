using AccommodationBookingPlatform.Domain.Common;
using System.Linq.Expressions;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{

    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAsync(Query<TEntity> query, CancellationToken cancellationToken = default);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    }
}
