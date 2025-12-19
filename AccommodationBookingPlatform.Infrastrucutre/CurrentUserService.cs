using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
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


        public string? Role =>
            _httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.Role)?.Value;

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
