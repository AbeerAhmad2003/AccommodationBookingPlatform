namespace AccommodationBookingPlatform.Application.Contracts.Services.Pricing
{
    public record PricingResult(
         decimal OriginalPricePerNight,
         decimal? DiscountPercentage,
         decimal FinalPricePerNight,
         int Nights,
         int RoomsCount,
         decimal TotalPrice);
}
