using AccommodationBookingPlatform.Domain.Common;

namespace AccommodationBookingPlatform.Domain.Entities
{
    public class Amenity : EntityBase, IAuditableEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<RoomClass> RoomClasses { get; set; } = new List<RoomClass>();
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
