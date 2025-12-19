namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelDetails
{
    public class HotelDetailsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CityName { get; set; }
        public string Country { get; set; }

        public double ReviewsRating { get; set; }
        public int ReviewsCount { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string? BriefDescription { get; set; }
        public string? Description { get; set; }
        public string PhoneNumber { get; set; }

        public string? ThumbnailUrl { get; set; }
        public List<string> GalleryUrls { get; set; } = new();

        public List<RoomClassDto> RoomClasses { get; set; } = new();
        public List<ReviewDto> Reviews { get; set; } = new();
    }
}
