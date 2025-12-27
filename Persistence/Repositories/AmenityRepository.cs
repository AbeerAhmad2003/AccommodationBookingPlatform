using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class AmenityRepository : BaseRepository<Amenity>, IAmenityRepository
    {
        private readonly AccommodationBookingDbContext _context;

        public AmenityRepository(AccommodationBookingDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Amenity>> GetAllAsync(
            CancellationToken ct = default)
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Amenity>> GetByRoomClassIdAsync(
            Guid roomClassId,
            CancellationToken ct = default)
        {
            // Many-to-Many بين Amenity و RoomClass
            return await _dbSet
                .Where(a => a.RoomClasses.Any(rc => rc.Id == roomClassId))
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
