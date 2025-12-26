using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Services.Pricing
{
    public class FeaturedDealCalculator : IFeaturedDealCalculator
    {
        public (decimal Original, decimal Discounted) CalculateBestDeal(
            Hotel hotel,
            DateTime nowUtc)
        {
            // لو الفندق ما عنده روم كلاس أصلاً
            if (hotel.RoomClasses == null || !hotel.RoomClasses.Any())
                return (0, 0);

            // جيبي كل الخصومات الفعّالة حالياً
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

            // 🔥 لو ما في خصومات → رجع أقل سعر متوفر بدون خصم
            if (!activeDeals.Any())
            {
                var minPrice = hotel.RoomClasses.Min(rc => rc.PricePerNight);
                return (minPrice, minPrice);
            }

            // 🔥 غير هيك → رجّع أفضل ديل
            var bestDeal = activeDeals
                .OrderBy(d => d.Discounted)
                .First();

            return (bestDeal.Original, bestDeal.Discounted);
        }
    }
}
