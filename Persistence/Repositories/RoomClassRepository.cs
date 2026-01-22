using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class RoomClassRepository
        : BaseRepository<RoomClass>, IRoomClassRepository
    {
        private readonly AccommodationBookingDbContext _context;

        public RoomClassRepository(AccommodationBookingDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameInHotelAsync(
            Guid hotelId,
            string name,
            CancellationToken ct = default)
        {
            return await _context.RoomClasses
                .AnyAsync(rc =>
                    rc.HotelId == hotelId &&
                    rc.Name == name,
                    ct);
        }

        public async Task<IReadOnlyList<RoomClass>> GetByHotelIdAsync(
            Guid hotelId,
            CancellationToken ct = default)
        {
            return await _context.RoomClasses
                .Where(rc => rc.HotelId == hotelId)
                .Include(rc => rc.Rooms)
                .Include(rc => rc.Hotel)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<RoomClass?> GetByIdWithDetailsAsync(
            Guid id,
            CancellationToken ct = default)
        {
            return await _context.RoomClasses
                .Include(rc => rc.Hotel)
                .Include(rc => rc.Rooms)
                .Include(rc => rc.Amenities)
                .FirstOrDefaultAsync(rc => rc.Id == id, ct);
        }
    }
}

