using AccommodationBookingPlatform.Application.Email;

namespace AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage message, CancellationToken ct = default);
    }
}
