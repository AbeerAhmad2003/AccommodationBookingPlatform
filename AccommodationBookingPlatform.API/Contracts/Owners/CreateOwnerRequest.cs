namespace AccommodationBookingPlatform.API.Contracts.Owners
{
    public class CreateOwnerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
