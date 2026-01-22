namespace AccommodationBookingPlatform.Domain.Entities
{
    namespace AccommodationBookingPlatform.Domain.Entities
    {
        public class BookingRoom
        {
            public Guid BookingId { get; set; }
            public Booking Booking { get; set; }

            public Guid RoomId { get; set; }
            public Room Room { get; set; }

            public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        }
    }

}
