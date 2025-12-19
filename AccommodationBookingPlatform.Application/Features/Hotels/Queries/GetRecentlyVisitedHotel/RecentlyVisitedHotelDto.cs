namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetRecentlyVisitedHotel
{
    public class RecentlyVisitedHotelDto
    {
        public Guid HotelId { get; init; }
        public Guid BookingId { get; init; }

        public string Name { get; init; }
        public string CityName { get; init; }
        public string Country { get; init; }

        public double ReviewsRating { get; init; }

        public DateTime CheckInDateUtc { get; init; }
        public DateTime CheckOutDateUtc { get; init; }

        public decimal TotalPrice { get; init; }

        public string? ThumbnailUrl { get; init; }
    }
}
