using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Services.Pricing
{
    public interface IBookingPricingService
    {
        Task<PricingResult> CalculateAsync(
              Hotel hotel,
              RoomClass roomClass,
              DateTime checkIn,
              DateTime checkOut,
              int roomsCount,
              int adults,
              int children,
              CancellationToken ct = default);


        Task<decimal> CalculateTotalPriceAsync(
            Hotel hotel,
            RoomClass roomClass,
            DateTime checkIn,
            DateTime checkOut,
            int roomsCount,
            int adults,
            int children,
            CancellationToken ct = default);
    }
}
