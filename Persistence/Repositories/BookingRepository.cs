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

        // 🔹 Checkout
        public async Task<Booking> CreateAsync(
            Booking booking,
            CancellationToken ct = default)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(ct);
            return booking;
        }

        // 🔹 Confirmation Page
        public async Task<Booking?> GetByIdWithDetailsAsync(
     Guid bookingId,
     CancellationToken ct = default)
        {
            return await _context.Bookings
                .Include(b => b.Hotel)
                .Include(b => b.Rooms)          // إذا بتربطي غرف بالحجز
                .Include(b => b.InvoiceRecords) // للتأكيد والفاتورة
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == bookingId, ct);
        }

        // 🔹 User Booking History
        public async Task<IReadOnlyList<Booking>> GetByUserIdAsync(
            Guid userId,
            CancellationToken ct = default)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAtUtc)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        // 🔹 Admin Bookings Grid
        public async Task<PaginatedList<Booking>> GetBookingsAsync(
            Query<Booking> query,
            CancellationToken ct = default)
        {
            IQueryable<Booking> bookings = _context.Bookings;

            if (query.Filter != null)
                bookings = bookings.Where(query.Filter);

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

        // 🔹 Admin delete / cancel
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
            // Guard فقط (مش business validation)
            if (from >= to)
                throw new ArgumentException("Invalid date range.");

            // 1️⃣ عدد الغرف الكلي بالفندق
            var totalRooms = await _context.Rooms
                .CountAsync(r => r.RoomClass.HotelId == hotelId, ct);

            // 2️⃣ مجموع الغرف المحجوزة بنفس الفترة
            var bookedRooms = await _context.Bookings
                .Where(b =>
                    b.HotelId == hotelId &&
                    b.CheckInDate < to &&
                    b.CheckOutDate > from
                )
                .SumAsync(b => b.RoomsCount, ct);

            // 3️⃣ المتاح
            var availableRooms = totalRooms - bookedRooms;

            return availableRooms >= requestedRooms;
        }
        public async Task<IReadOnlyList<Booking>> GetRecentBookingsInDifferentHotelsByUserId(
       Guid userId,
       int count,
       CancellationToken ct = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            // 1️⃣ نجيب كل حجوزات اليوزر مرتبة من الأحدث للأقدم
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAtUtc)
                .Include(b => b.Hotel)
                    .ThenInclude(h => h.City)
                .Include(b => b.Hotel)
                    .ThenInclude(h => h.Thumbnail)
                .AsNoTracking()
                .ToListAsync(ct);

            // 2️⃣ نختار حجز واحد فقط لكل فندق (أحدث حجز)
            var distinctBookings = bookings
                .GroupBy(b => b.HotelId)
                .Select(g => g.First())
                .Take(count)
                .ToList();

            return distinctBookings;
        }



    }

}
