using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Services.Pricing
{
    public class BookingPricingService : IBookingPricingService
    {
        public Task<decimal> CalculateTotalPriceAsync(
            Hotel hotel,
            DateTime checkIn,
            DateTime checkOut,
            int roomsCount,
            int adults,
            int children,
            CancellationToken ct = default)
        {
            var nights = (checkOut.Date - checkIn.Date).Days;

            if (nights <= 0)
                throw new ArgumentException("Invalid stay duration.");

            var basePrice = hotel.RoomClasses.Min(rc => rc.PricePerNight);

            decimal total = basePrice * roomsCount * nights;

            return Task.FromResult(total);
        }
    }
}
