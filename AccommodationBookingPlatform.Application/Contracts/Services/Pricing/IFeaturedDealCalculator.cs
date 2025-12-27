using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Services.Pricing
{
    public interface IFeaturedDealCalculator
    {
        (decimal Original, decimal Discounted) CalculateBestDeal(
         Hotel hotel,
         DateTime nowUtc);
    }
}
