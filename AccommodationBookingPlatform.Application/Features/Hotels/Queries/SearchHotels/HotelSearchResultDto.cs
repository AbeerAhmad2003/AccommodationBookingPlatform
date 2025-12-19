namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.SearchHotels
{
    public class HotelSearchResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public double Rating { get; set; }
        public string ThumbnailUrl { get; set; }
        public decimal PriceFrom { get; set; }
        public string? BriefDescription { get; set; }
    }
}
