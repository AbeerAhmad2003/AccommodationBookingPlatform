namespace AccommodationBookingPlatform.API.Contracts.Cities
{
    public class UpdateCityRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string PostOffice { get; set; }
    }

}
