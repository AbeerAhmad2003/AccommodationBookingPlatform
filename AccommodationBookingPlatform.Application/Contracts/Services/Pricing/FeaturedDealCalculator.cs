using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Services.Pricing
{
    public class FeaturedDealCalculator : IFeaturedDealCalculator
    {
        public (decimal Original, decimal Discounted) CalculateBestDeal(
            Hotel hotel,
            DateTime nowUtc)
        {
            var bestDeal = hotel.RoomClasses
                .SelectMany(rc =>
                    rc.Discounts
                        .Where(d =>
                            d.StartDateUtc <= nowUtc &&
                            d.EndDateUtc >= nowUtc)
                        .Select(d => new
                        {
                            Original = rc.PricePerNight,
                            Discounted = rc.PricePerNight *
                                         (1 - d.Percentage / 100m)
                        }))
                .OrderBy(x => x.Discounted)
                .First();

            return (bestDeal.Original, bestDeal.Discounted);
        }
    }


}
