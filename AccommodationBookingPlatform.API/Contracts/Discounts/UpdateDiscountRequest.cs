namespace AccommodationBookingPlatform.API.Contracts.Discounts
{
    public class UpdateDiscountRequest
    {
        public decimal Percentage { get; set; }
        public DateTime StartDateUtc { get; set; }
        public DateTime EndDateUtc { get; set; }
    }

}
