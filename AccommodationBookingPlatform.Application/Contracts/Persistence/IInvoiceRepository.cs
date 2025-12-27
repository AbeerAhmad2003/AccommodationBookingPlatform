using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IInvoiceRepository : IRepository<InvoiceRecord>
    {
        Task<InvoiceRecord?> GetInvoiceByBookingIdAsync(
            Guid bookingId,
            CancellationToken ct = default);
    }
}

