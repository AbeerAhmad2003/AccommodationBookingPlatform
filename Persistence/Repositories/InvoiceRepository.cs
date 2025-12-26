using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class InvoiceRepository : BaseRepository<InvoiceRecord>, IInvoiceRepository
    {
        private readonly AccommodationBookingDbContext _context;

        public InvoiceRepository(AccommodationBookingDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<InvoiceRecord>> GetByBookingIdAsync(
            Guid bookingId,
            CancellationToken ct = default)
        {
            return await _context.InvoiceRecords
                .Where(i => i.BookingId == bookingId)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
