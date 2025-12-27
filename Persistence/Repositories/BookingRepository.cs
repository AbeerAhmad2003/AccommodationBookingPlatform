using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        private readonly AccommodationBookingDbContext _context;

        public BookingRepository(AccommodationBookingDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Booking> CreateAsync(
            Booking booking,
            CancellationToken ct = default)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(ct);
            return booking;
        }

        public async Task<Booking?> GetByIdWithDetailsAsync(
       Guid bookingId,
       CancellationToken ct = default)
        {
            return await _context.Bookings
                .Include(b => b.Hotel)
                    .ThenInclude(h => h.City)
                .Include(b => b.Hotel)
                    .ThenInclude(h => h.RoomClasses)
                        .ThenInclude(rc => rc.Discounts)
                .Include(b => b.Hotel)
                    .ThenInclude(h => h.RoomClasses)
                        .ThenInclude(rc => rc.Rooms)
                .Include(b => b.InvoiceRecords)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == bookingId, ct);
        }

        public async Task<IReadOnlyList<Booking>> GetByUserIdAsync(
            Guid userId,
            CancellationToken ct = default)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Hotel)
                    .ThenInclude(h => h.City)
                .OrderByDescending(b => b.CreatedAtUtc)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<PaginatedList<Booking>> GetBookingsAsync(
            Query<Booking> query,
            CancellationToken ct = default)
        {
            IQueryable<Booking> bookings = _context.Bookings
                .Include(b => b.Hotel)
                    .ThenInclude(h => h.City)
                    .Include(b => b.User);

            if (query.Filter != null)
                bookings = bookings.Where(query.Filter);

            bookings = bookings.OrderByDescending(b => b.CreatedAtUtc);

            var totalCount = await bookings.CountAsync(ct);

            var items = await bookings
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .AsNoTracking()
                .ToListAsync(ct);

            return new PaginatedList<Booking>(
                items,
                totalCount,
                query.PageNumber,
                query.PageSize);
        }

        public async Task DeleteAsync(
            Guid bookingId,
            CancellationToken ct = default)
        {
            var booking = await _context.Bookings
                .FindAsync(new object[] { bookingId }, ct);

            if (booking == null)
                return;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> IsHotelAvailableAsync(
            Guid hotelId,
            DateTime from,
            DateTime to,
            int requestedRooms,
            CancellationToken ct = default)
        {
            if (from >= to)
                throw new ArgumentException("Invalid date range.");

            var totalRooms = await _context.Rooms
                .CountAsync(r => r.RoomClass.HotelId == hotelId, ct);

            var bookedRooms = await _context.Bookings
                .Where(b =>
                    b.HotelId == hotelId &&
                    b.CheckInDate < to &&
                    b.CheckOutDate > from
                )
                .SumAsync(b => b.RoomsCount, ct);

            var availableRooms = totalRooms - bookedRooms;

            return availableRooms >= requestedRooms;
        }

        public async Task<IReadOnlyList<Booking>> GetRecentBookingsInDifferentHotelsByUserId(
            Guid userId,
            int count,
            CancellationToken ct = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAtUtc)
                .Include(b => b.Hotel)
                    .ThenInclude(h => h.City)
                .AsNoTracking()
                .ToListAsync(ct);

            var distinctBookings = bookings
                .GroupBy(b => b.HotelId)
                .Select(g => g.First())
                .Take(count)
                .ToList();

            return distinctBookings;
        }
        public async Task<bool> IsRoomClassAvailableAsync(
    Guid roomClassId,
    DateTime from,
    DateTime to,
    int requestedRooms,
    CancellationToken ct = default)
        {
            if (from >= to)
                throw new ArgumentException("Invalid date range.");

            // total rooms in this room class only
            var totalRooms = await _context.Rooms
                .CountAsync(r => r.RoomClassId == roomClassId, ct);

            // booked rooms in this room class in overlapping period
            var bookedRooms = await _context.Bookings
                .Where(b =>
                    b.RoomClassId == roomClassId &&
                    b.CheckInDate < to &&
                    b.CheckOutDate > from
                )
                .SumAsync(b => b.RoomsCount, ct);

            var availableRooms = totalRooms - bookedRooms;

            return availableRooms >= requestedRooms;
        }

    }

}
