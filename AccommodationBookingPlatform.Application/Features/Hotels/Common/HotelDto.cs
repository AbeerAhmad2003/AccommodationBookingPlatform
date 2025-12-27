namespace AccommodationBookingPlatform.Application.Features.Hotels.Common
{
    public class HotelDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CityId { get; set; }
        public string CityName { get; set; }

        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }

        public double ReviewsRating { get; set; }
        public int ReviewsCount { get; set; }

        public int RoomsCount { get; set; }

        public string PhoneNumber { get; set; }

        public string? BriefDescription { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }

}
