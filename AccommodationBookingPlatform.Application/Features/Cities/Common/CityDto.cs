namespace AccommodationBookingPlatform.Application.Features.Cities.Common
{
    public class CityDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }
        public string PostOffice { get; set; }

        public int HotelsCount { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }

        public string? ThumbnailUrl { get; set; }
    }
}
