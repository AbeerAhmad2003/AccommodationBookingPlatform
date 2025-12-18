using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
     where TEntity : EntityBase
    {
        protected readonly AccommodationBookingDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(AccommodationBookingDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct)
            => await _dbSet.FindAsync(new object[] { id }, ct);

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter,
            CancellationToken ct)
            => await _dbSet
                .Where(filter ?? (_ => true))
                .AsNoTracking()
                .ToListAsync(ct);

        public async Task<bool> IsExistAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken ct)
            => await _dbSet.AnyAsync(predicate, ct);

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return entity;
        }
    }


}
