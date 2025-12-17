using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
     where TEntity : class
    {
        protected readonly AccommodationBookingDbContext _context;

        public BaseRepository(AccommodationBookingDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);

        public async Task<IEnumerable<TEntity>> GetAsync(
            Query<TEntity> query,
            CancellationToken cancellationToken)
        {
            IQueryable<TEntity> q = _context.Set<TEntity>();

            if (query.Filter != null)
                q = q.Where(query.Filter);

            if (!string.IsNullOrWhiteSpace(query.SortColumn))
                q = query.SortOrder == SortOrder.Desc
                    ? q.OrderByDescending(e => EF.Property<object>(e, query.SortColumn))
                    : q.OrderBy(e => EF.Property<object>(e, query.SortColumn));

            return await q
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await _context.Set<TEntity>().AnyAsync(predicate, cancellationToken);

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }

}
