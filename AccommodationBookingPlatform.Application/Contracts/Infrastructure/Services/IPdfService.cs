using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services
{
    public interface IPdfService
    {
        byte[] GenerateInvoicePdf(InvoiceRecord invoice);
    }

}
