using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Domain.Entities;
using QuestPDF.Fluent;
namespace AccommodationBookingPlatform.Infrastrucutre.PDF
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateInvoicePdf(InvoiceRecord invoice)
        {
            var pdf = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);

                    page.Header()
                        .Text($"Invoice #{invoice.InvoiceNumber}")
                        .FontSize(22)
                        .Bold();

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"User Id: {invoice.UserId}");
                        col.Item().Text($"Booking Id: {invoice.BookingId}");
                        col.Item().Text($"Total: {invoice.TotalAmount} $");
                        col.Item().Text($"Payment: {invoice.PaymentMethod}");
                        col.Item().Text($"Created: {invoice.CreatedAtUtc}");
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Accommodation Booking System")
                        .FontSize(10);
                });
            });

            return pdf.GeneratePdf();
        }

    }
}
