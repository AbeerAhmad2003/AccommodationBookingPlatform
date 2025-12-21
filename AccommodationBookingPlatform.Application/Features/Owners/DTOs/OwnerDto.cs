namespace AccommodationBookingPlatform.Application.Features.Owners.DTOs
{
    public class OwnerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
