using AccommodationBookingPlatform.Domain.Common.Enums;

namespace AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }
        UserRole? Role { get; }
        bool IsAuthenticated { get; }
    }
}
