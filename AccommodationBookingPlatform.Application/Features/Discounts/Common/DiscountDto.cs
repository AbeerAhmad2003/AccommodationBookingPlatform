namespace AccommodationBookingPlatform.Application.Features.Discounts.Common
{
    public class DiscountDto
    {
        public Guid Id { get; set; }

        public Guid RoomClassId { get; set; }
        public string RoomClassName { get; set; }

        public decimal Percentage { get; set; }

        public DateTime StartDateUtc { get; set; }
        public DateTime EndDateUtc { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }

}
