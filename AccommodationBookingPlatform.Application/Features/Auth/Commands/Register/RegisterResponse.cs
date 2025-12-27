namespace AccommodationBookingPlatform.Application.Features.Auth.Commands.Register
{
    public class RegisterResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
