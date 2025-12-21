using AccommodationBookingPlatform.Application.Contracts.Persistence;
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
    }
}
