namespace AccommodationBookingPlatform.Application.Contracts.Services
{
    internal interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        Task<AuthResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    }
}
}
