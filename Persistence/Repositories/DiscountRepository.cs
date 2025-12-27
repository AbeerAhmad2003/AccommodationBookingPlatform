using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class DiscountRepository
     : BaseRepository<Discount>, IDiscountRepository
    {
        private readonly AccommodationBookingDbContext _context;

        public DiscountRepository(AccommodationBookingDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Discount>> FindAsync(
    Expression<Func<Discount, bool>> predicate,
    CancellationToken ct = default)
        {
            return await _context.Discounts
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync(ct);
        }


    }

}
