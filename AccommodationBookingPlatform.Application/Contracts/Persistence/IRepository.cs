using AccommodationBookingPlatform.Domain.Common;
using System.Linq.Expressions;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{

    public interface IRepository<TEntity>
      where TEntity : EntityBase
    {
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter,
            CancellationToken ct = default);

        Task<bool> IsExistAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken ct = default);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = default);
    }

}
