namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetBookingDetails
{
    public class InvoiceDto
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }

}
