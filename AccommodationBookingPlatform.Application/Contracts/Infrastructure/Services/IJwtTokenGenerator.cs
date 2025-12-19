using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
