using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Domain.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace AccommodationBookingPlatform.Infrastrucutre
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId =>
            Guid.TryParse(
                _httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var userId)
            ? userId
            : null;

        public string? Email =>
            _httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.Email)?.Value;


        public UserRole? Role
        {
            get
            {
                var roleClaim = _httpContextAccessor.HttpContext?.User?
                    .FindFirst(ClaimTypes.Role)?.Value;

                if (string.IsNullOrWhiteSpace(roleClaim))
                    return null;

                return Enum.TryParse<UserRole>(roleClaim, out var role)
                    ? role
                    : null;
            }
        }

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
