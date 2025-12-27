namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelDetails
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string GuestName { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
