using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class RoomRepository
     : BaseRepository<Room>, IRoomRepository
    {
        private readonly AccommodationBookingDbContext _context;

        public RoomRepository(AccommodationBookingDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<int> GetTotalRoomsByHotelAsync(
            Guid hotelId,
            CancellationToken ct = default)
        {
            return await _context.Rooms
                .CountAsync(r => r.RoomClass.HotelId == hotelId, ct);
        }

        public async Task<bool> ExistsNumberInRoomClassAsync(
            Guid roomClassId,
            string number,
            CancellationToken ct = default)
        {
            return await _context.Rooms
                .AnyAsync(r =>
                    r.RoomClassId == roomClassId &&
                    r.Number == number,
                    ct);
        }

        public async Task<IReadOnlyList<Room>> GetByRoomClassIdAsync(
            Guid roomClassId,
            CancellationToken ct = default)
        {
            return await _context.Rooms
                .Where(r => r.RoomClassId == roomClassId)
                .Include(r => r.RoomClass)
                .AsNoTracking()
                .ToListAsync(ct);
        }
        public async Task<bool> ExistsNumberInRoomClassForAnotherRoomAsync(
    Guid roomClassId,
    string number,
    Guid roomId,
    CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .AnyAsync(r =>
                    r.RoomClassId == roomClassId &&
                    r.Number == number &&
                    r.Id != roomId,
                    cancellationToken);
        }

    }


}
