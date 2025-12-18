namespace AccommodationBookingPlatform.Application.Features.Cities.Queries.GetTrendingCities
{
    public class TrendingCityDto
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
