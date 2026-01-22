using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities.AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Domain.Entities
{
    public class Room : EntityBase, IAuditableEntity
    {
        public Guid RoomClassId { get; set; }
        public RoomClass RoomClass { get; set; }
        public string Number { get; set; }
        public ICollection<BookingRoom> BookingRooms { get; set; } = new List<BookingRoom>();
        //public ICollection<InvoiceRecord> InvoiceRecords { get; set; } = new List<InvoiceRecord>();
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
