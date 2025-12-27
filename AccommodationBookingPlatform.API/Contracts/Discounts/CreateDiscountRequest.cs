namespace AccommodationBookingPlatform.API.Contracts.Discounts
{
    public class CreateDiscountRequest
    {
        public Guid RoomClassId { get; set; }

        public decimal Percentage { get; set; }

        public DateTime StartDateUtc { get; set; }

        public DateTime EndDateUtc { get; set; }
    }

}
