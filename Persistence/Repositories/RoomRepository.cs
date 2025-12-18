using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
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
    }

}
