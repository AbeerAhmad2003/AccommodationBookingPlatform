using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Services.Pricing
{
    public class BookingPricingService : IBookingPricingService
    {

        public async Task<PricingResult> CalculateAsync(
            Hotel hotel,
            RoomClass roomClass,
            DateTime checkIn,
            DateTime checkOut,
            int roomsCount,
            int adults,
            int children,
            CancellationToken ct = default)
        {

            await Task.CompletedTask;

            if (checkOut <= checkIn)
                throw new ArgumentException("Checkout must be after Checkin");

            int nights = (checkOut - checkIn).Days;

            decimal originalPricePerNight = roomClass.PricePerNight;

            var nowUtc = DateTime.UtcNow;

            var discount = roomClass.Discounts?
                .FirstOrDefault(d =>
                   d.StartDateUtc < checkOut &&
        d.EndDateUtc > checkIn);

            decimal? discountPercent = discount?.Percentage;

            decimal finalPricePerNight = originalPricePerNight;

            if (discountPercent is not null)
            {
                finalPricePerNight =
                    originalPricePerNight * (1 - (discountPercent.Value / 100m));
            }

            decimal total = finalPricePerNight * nights * roomsCount;

            return new PricingResult(
                OriginalPricePerNight: originalPricePerNight,
                DiscountPercentage: discountPercent,
                FinalPricePerNight: finalPricePerNight,
                Nights: nights,
                RoomsCount: roomsCount,
                TotalPrice: total
            );
        }

        public async Task<decimal> CalculateTotalPriceAsync(
            Hotel hotel,
            RoomClass roomClass,
            DateTime checkIn,
            DateTime checkOut,
            int roomsCount,
            int adults,
            int children,
            CancellationToken ct = default)
        {
            var result = await CalculateAsync(
                hotel,
                roomClass,
                checkIn,
                checkOut,
                roomsCount,
                adults,
                children,
                ct);

            return result.TotalPrice;
        }
    }
}

