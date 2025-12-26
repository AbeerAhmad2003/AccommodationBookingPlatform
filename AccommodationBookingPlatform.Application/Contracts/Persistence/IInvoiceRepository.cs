using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IInvoiceRepository : IRepository<InvoiceRecord>
    {
        Task<IReadOnlyList<InvoiceRecord>> GetByBookingIdAsync(
            Guid bookingId,
            CancellationToken ct = default);
    }
}
