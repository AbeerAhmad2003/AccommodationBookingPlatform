using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Common.Enums;

namespace AccommodationBookingPlatform.Domain.Entities
{
    public class InvoiceRecord : EntityBase, IAuditableEntity
    {
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public Guid UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string InvoiceNumber { get; set; } = null!; // INV-2025-0001

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
