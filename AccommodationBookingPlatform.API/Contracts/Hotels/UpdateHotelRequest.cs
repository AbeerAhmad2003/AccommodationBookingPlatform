namespace AccommodationBookingPlatform.API.Contracts.Hotels
{
    public class UpdateHotelRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public Guid OwnerId { get; set; }
        public string PhoneNumber { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? BriefDescription { get; set; }
        public string? Description { get; set; }
    }

}
