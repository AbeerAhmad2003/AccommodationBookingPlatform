using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Services.Pricing
{
    public interface IBookingPricingService
    {
        Task<decimal> CalculateTotalPriceAsync(
            Hotel hotel,
            DateTime checkIn,
            DateTime checkOut,
            int roomsCount,
            int adults,
            int children,
            CancellationToken ct = default);
    }
}
