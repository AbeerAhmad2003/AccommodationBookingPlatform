namespace AccommodationBookingPlatform.Application.Features.Auth.Commands.Login
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }

}
