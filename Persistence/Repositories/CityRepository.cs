using AccommodationBookingPlatform.Application.Contracts.Persistence;
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

        public async Task<IReadOnlyList<City>> GetMostVisitedAsync(
            int count,
            CancellationToken cancellationToken = default)
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

    }


}
