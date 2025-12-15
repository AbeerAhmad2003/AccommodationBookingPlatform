using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Common.Enums;

namespace AccommodationBookingPlatform.Domain.Entities
{
    public class Booking : EntityBase, IAuditableEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int RoomsCount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<InvoiceRecord> InvoiceRecords { get; set; } = new List<InvoiceRecord>();

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
