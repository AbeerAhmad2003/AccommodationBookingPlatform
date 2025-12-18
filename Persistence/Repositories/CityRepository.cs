using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Common.Enums;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        private readonly AccommodationBookingDbContext _context;

        public CityRepository(AccommodationBookingDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<City>> GetMostVisitedAsync(int count, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            var cityIds = await _context.Bookings
                .GroupBy(b => b.Hotel.CityId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(count)
                .ToListAsync(cancellationToken);

            var cities = await _context.Cities
                .Where(c => cityIds.Contains(c.Id))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var thumbnails = await _context.Images
                .Where(i => cityIds.Contains(i.CityId!.Value)
                            && i.Type == ImageType.Thumbnail)
                .ToListAsync(cancellationToken);

            foreach (var city in cities)
            {
                city.Thumbnail = thumbnails
                    .FirstOrDefault(i => i.CityId == city.Id);
            }

            return cities;
        }

        public async Task<PaginatedList<City>> GetCitiesAsync(Query<City> query, CancellationToken ct = default)
        {
            IQueryable<City> cities = _context.Cities;

            if (query.Filter != null)
                cities = cities.Where(query.Filter);

            var totalCount = await cities.CountAsync(ct);

            var items = await cities
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .AsNoTracking()
                .ToListAsync(ct);

            return new PaginatedList<City>(
                items,
                totalCount,
                query.PageNumber,
                query.PageSize);
        }

        public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken ct = default)
        {
            return await _context.Cities
                .AnyAsync(c => c.Name == name, ct);
        }
        public async Task<int> GetHotelsCountAsync(Guid cityId, CancellationToken ct = default)
        {
            return await _context.Hotels
                .CountAsync(h => h.CityId == cityId, ct);
        }

    }
}