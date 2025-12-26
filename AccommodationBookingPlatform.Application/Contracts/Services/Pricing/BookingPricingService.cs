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

            if (hotel.RoomClasses is null || !hotel.RoomClasses.Any())
                throw new InvalidOperationException("Hotel has no room classes.");

            var effectivePrices = new List<decimal>();

            foreach (var roomClass in hotel.RoomClasses)
            {
                decimal basePrice = roomClass.PricePerNight;

                // Get active discounts during requested stay
                var activeDiscounts = roomClass.Discounts
                    .Where(d =>
                        d.StartDateUtc <= checkIn &&
                        d.EndDateUtc >= checkOut)
                    .ToList();

                decimal effectivePrice = basePrice;

                if (activeDiscounts.Any())
                {
                    // Choose best discount (lowest resulting price)
                    effectivePrice = activeDiscounts
                        .Select(d => basePrice * (1 - d.Percentage / 100m))
                        .Min();
                }

                effectivePrices.Add(effectivePrice);
            }

            var bestPricePerNight = effectivePrices.Min();

            decimal total = bestPricePerNight * roomsCount * nights;

            return Task.FromResult(total);
        }
    }
}
