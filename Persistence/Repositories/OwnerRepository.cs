using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        private readonly AccommodationBookingDbContext _context;

        public OwnerRepository(AccommodationBookingDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Guid ownerId, CancellationToken ct = default)
        {
            return await _context.Owners.AnyAsync(o => o.Id == ownerId, ct);
        }
        public async Task<PaginatedList<Owner>> GetOwnersAsync(
        Query<Owner> query,
        CancellationToken ct = default)
        {
            IQueryable<Owner> owners = _context.Owners;

            // Apply filter 
            if (query.Filter != null)
                owners = owners.Where(query.Filter);

            var totalCount = await owners.CountAsync(ct);

            var items = await owners
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .AsNoTracking()
                .ToListAsync(ct);

            return new PaginatedList<Owner>(
                items,
                totalCount,
                query.PageNumber,
                query.PageSize
            );
        }
    }
}
