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

                    // Header
                    page.Header()
                        .PaddingBottom(20)
                        .Text($"Invoice #{invoice.InvoiceNumber}")
                        .FontSize(24)
                        .Bold()
                        .AlignCenter();



                    page.Content().Column(col =>
                    {
                        col.Spacing(12);

                        col.Item().Text($"User Id: {invoice.UserId}");
                        col.Item().Text($"Booking Id: {invoice.BookingId}");

                        col.Item().Text(text =>
                        {
                            text.Span("Payment Method: ").SemiBold();
                            text.Span(invoice.PaymentMethod.ToString());
                        });

                        col.Item().Text(text =>
                        {
                            text.Span("Created At: ").SemiBold();
                            text.Span(invoice.CreatedAtUtc.ToString("yyyy-MM-dd HH:mm:ss"));
                        });

                        col.Item().LineHorizontal(1);

                        // 🔥 Pricing Snapshot Section
                        col.Item().Text("Pricing Details").FontSize(16).Bold();

                        col.Item().Text(text =>
                        {
                            text.Span("Price Per Night (Before Discount): ").SemiBold();
                            text.Span($"{invoice.PricePerNightBeforeDiscount} $");
                        });

                        col.Item().Text(text =>
                        {
                            text.Span("Discount Applied: ").SemiBold();
                            text.Span(invoice.DiscountPercentageApplied is null
                                ? "No discount"
                                : $"{invoice.DiscountPercentageApplied}%");
                        });

                        col.Item().Text(text =>
                        {
                            text.Span("Final Price Per Night: ").SemiBold();
                            text.Span($"{invoice.FinalPricePerNight} $");
                        });

                        col.Item().Text(text =>
                        {
                            text.Span("Nights: ").SemiBold();
                            text.Span($"{invoice.Nights}");
                        });

                        col.Item().Text(text =>
                        {
                            text.Span("Rooms Count: ").SemiBold();
                            text.Span($"{invoice.RoomsCount}");
                        });

                        col.Item().LineHorizontal(1);

                        // 🔥 TOTAL
                        col.Item().Text(text =>
                        {
                            text.Span("Total Amount: ").FontSize(16).SemiBold();
                            text.Span($" {invoice.TotalAmount} $").FontSize(16).Bold();
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Accommodation Booking System — Invoice Document")
                        .FontSize(10);
                });
            });

            return pdf.GeneratePdf();
        }
    }
}