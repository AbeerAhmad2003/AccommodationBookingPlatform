using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Invoices.Queries.GetInvoicePdf
{
    public class GetInvoicePdfQueryHandler
    : IRequestHandler<GetInvoicePdfQuery, InvoicePdfResult>
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IPdfService _pdfService;
        private readonly ICurrentUserService _currentUser;

        public GetInvoicePdfQueryHandler(
            IInvoiceRepository invoiceRepo,
            IPdfService pdfService,
            ICurrentUserService currentUser)
        {
            _invoiceRepo = invoiceRepo;
            _pdfService = pdfService;
            _currentUser = currentUser;
        }

        public async Task<InvoicePdfResult> Handle(
            GetInvoicePdfQuery request,
            CancellationToken ct)
        {
            var invoice = await _invoiceRepo.GetInvoiceByBookingIdAsync(request.BookingId, ct);

            if (invoice is null)
                throw new NotFoundException("Invoice", request.BookingId);

            // Security 
            var isAdmin = _currentUser.Role == Domain.Common.Enums.UserRole.Admin;

            if (!isAdmin && invoice.UserId != _currentUser.UserId)
                throw new ForbiddenException("Access denied.");

            var pdfBytes = _pdfService.GenerateInvoicePdf(invoice);
            return new InvoicePdfResult
            {
                Content = pdfBytes,
                FileName = $"{invoice.InvoiceNumber}.pdf"
            };
        }
    }
}
