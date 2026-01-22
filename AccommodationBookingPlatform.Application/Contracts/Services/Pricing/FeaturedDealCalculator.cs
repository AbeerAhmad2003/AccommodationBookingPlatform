using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Services.Pricing
{
    public class FeaturedDealCalculator : IFeaturedDealCalculator
    {
        public (decimal Original, decimal Discounted) CalculateBestDeal(
            Hotel hotel,
            DateTime nowUtc)
        {
            // If the hotel has no room classes at all
            if (hotel.RoomClasses == null || !hotel.RoomClasses.Any())
                return (0, 0);

            // Collect all currently active discounts across all room classes
            var activeDeals = hotel.RoomClasses
                .SelectMany(rc =>
                    rc.Discounts != null
                        ? rc.Discounts
                            .Where(d => d.StartDateUtc <= nowUtc &&
                                        d.EndDateUtc >= nowUtc)
                            .Select(d => new
                            {
                                Original = rc.PricePerNight,
                                Discounted = rc.PricePerNight * (1 - d.Percentage / 100m)
                            })
                        : Enumerable.Empty<dynamic>()
                )
                .ToList();

            // If there are no active discounts, return the lowest available price without discount
            if (!activeDeals.Any())
            {
                var minPrice = hotel.RoomClasses.Min(rc => rc.PricePerNight);
                return (minPrice, minPrice);
            }

            // Otherwise, return the best deal (lowest discounted price)
            var bestDeal = activeDeals
                .OrderBy(d => d.Discounted)
                .First();

            return (bestDeal.Original, bestDeal.Discounted);
        }
    }
}
