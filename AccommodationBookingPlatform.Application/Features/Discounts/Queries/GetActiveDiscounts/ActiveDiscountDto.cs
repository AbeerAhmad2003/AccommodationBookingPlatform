namespace AccommodationBookingPlatform.Application.Features.Discounts.Queries.GetActiveDiscounts
{
    public class ActiveDiscountDto
    {
        public Guid DiscountId { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDateUtc { get; set; }
        public DateTime EndDateUtc { get; set; }
    }

}
