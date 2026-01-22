using AccommodationBookingPlatform.Application.Email;

namespace AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services
{
    public interface IEmailService
    {
        Task SendBookingConfirmationAsync(
           string toEmail,
           string hotelName,
           string roomClassName,
           DateTime checkIn,
           DateTime checkOut,
           int nights,
           int roomsCount,
           IEnumerable<string> allocatedRoomNumbers,
           decimal originalPricePerNight,
           decimal? discountPercentage,
           decimal finalPricePerNight,
           decimal totalPrice,
           string invoiceNumber,
           DateTime invoiceCreatedAtUtc,
           CancellationToken ct = default);
        Task SendAsync(EmailMessage message, CancellationToken ct = default);
    }
}
