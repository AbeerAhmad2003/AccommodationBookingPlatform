using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Invoices.Queries.GetInvoicePdf
{
    public record GetInvoicePdfQuery(Guid BookingId)
      : IRequest<InvoicePdfResult>;

}