using AccommodationBookingPlatform.Domain.Common;

namespace AccommodationBookingPlatform.Domain.Entities
{
    public class Hotel : EntityBase, IAuditableEntity
    {
        public Guid CityId { get; set; }
        public City City { get; set; }

        public Guid OwnerId { get; set; }
        public Owner Owner { get; set; }

        public Image? Thumbnail { get; set; }
        public ICollection<Image> Gallery { get; set; } = new List<Image>();
        public ICollection<RoomClass> RoomClasses { get; set; } = new List<RoomClass>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public string Name { get; set; }
        //public double StarRating { get; set; }
        public double ReviewsRating { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? BriefDescription { get; set; }
        public string? Description { get; set; }
        public string PhoneNumber { get; set; }
        public int ReviewsCount { get; private set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public void AddReview(double rating)
        {
            ReviewsRating = ((ReviewsRating * ReviewsCount) + rating) / (ReviewsCount + 1);
            ReviewsCount++;
        }
    }
}
