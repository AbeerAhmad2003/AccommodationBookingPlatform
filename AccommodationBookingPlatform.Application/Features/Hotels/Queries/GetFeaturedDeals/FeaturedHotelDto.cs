namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetFeaturedDeals
{
    public class FeaturedHotelDto
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public double Rating { get; set; }

        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }

        public string? ThumbnailUrl { get; set; }
    }

}
