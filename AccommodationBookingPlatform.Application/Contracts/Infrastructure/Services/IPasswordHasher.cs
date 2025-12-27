namespace AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}
