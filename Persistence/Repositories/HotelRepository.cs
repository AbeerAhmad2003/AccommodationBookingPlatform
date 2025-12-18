using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Common.Enums;
using AccommodationBookingPlatform.Domain.Entities;
using AccommodationBookingPlatform.Persistence.Sorting;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class HotelRepository : BaseRepository<Hotel>, IHotelRepository
    {
        private readonly AccommodationBookingDbContext _context;

        public HotelRepository(AccommodationBookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Hotel>> SearchAsync(
            Query<Hotel> query,
            CancellationToken cancellationToken = default)
        {
            // 1️⃣ base query
            IQueryable<Hotel> hotelsQuery = _context.Hotels.AsQueryable();

            // 2️⃣ filter
            if (query.Filter != null)
            {
                hotelsQuery = hotelsQuery.Where(query.Filter);
            }

            // 3️⃣ total count (قبل الباجينيشن)
            var totalCount = await hotelsQuery.CountAsync(cancellationToken);

            // 4️⃣ sorting (لو عندك extension)
            if (!string.IsNullOrWhiteSpace(query.SortColumn))
            {
                var sortExpression =
                  HotelSortingExpressions.Get(query.SortColumn);

                hotelsQuery = query.SortOrder == SortOrder.Desc
                    ? hotelsQuery.OrderByDescending(sortExpression)
                    : hotelsQuery.OrderBy(sortExpression);
            }

            // 5️⃣ pagination
            var items = await hotelsQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // 6️⃣ رجوع PaginatedList
            return new PaginatedList<Hotel>(
                items,
                totalCount,
                query.PageNumber,
                query.PageSize);
        }

        public async Task<IEnumerable<Hotel>> GetFeaturedDealsAsync(
            int count,
            CancellationToken cancellationToken = default)
        {
            return await _context.Hotels
                .Where(h => h.RoomClasses.Any(rc => rc.Discounts.Any()))
                .OrderByDescending(h => h.ReviewsRating)
                .Take(count)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Hotel>> GetRecentlyVisitedAsync(
            Guid userId,
            int count,
            CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAtUtc)
                .Select(b => b.Hotel)
                .Distinct()
                .Take(count)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateReviewById(
            Guid hotelId,
            double newRating,
            CancellationToken cancellationToken = default)
        {
            var hotel = await _context.Hotels
                .FirstAsync(h => h.Id == hotelId, cancellationToken);

            hotel.AddReview(newRating);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaginatedList<Hotel>> GetHotelsAsync(
    Query<Hotel> query,
    CancellationToken ct = default)
        {
            IQueryable<Hotel> hotels = _context.Hotels;

            if (query.Filter != null)
                hotels = hotels.Where(query.Filter);

            var totalCount = await hotels.CountAsync(ct);

            var items = await hotels
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .AsNoTracking()
                .ToListAsync(ct);

            return new PaginatedList<Hotel>(
                items,
                totalCount,
                query.PageNumber,
                query.PageSize);
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid cityId, CancellationToken ct)
        {
            return await _context.Hotels.AnyAsync(h => h.Name == name && h.CityId == cityId, ct);
        }

        public async Task<Hotel?> GetHotelDetailsAsync(
     Guid hotelId,
     CancellationToken ct = default)
        {
            return await _context.Hotels
                .Include(h => h.RoomClasses)
                .Include(h => h.Reviews)
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == hotelId, ct);
        }


        public async Task DeleteHotelAsync(Guid hotelId, CancellationToken ct)
        {
            var hotel = await _context.Hotels.FindAsync(new object[] { hotelId }, ct);
            if (hotel == null) return;

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync(ct);
        }
    }


}
