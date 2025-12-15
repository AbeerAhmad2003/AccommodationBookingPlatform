using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Common.Enums;

namespace AccommodationBookingPlatform.Domain.Entities
{
    public class User : EntityBase, IAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public UserRole Role { get; set; }

        // Navigation
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
