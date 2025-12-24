namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUserBookings
{
    public class UserBookingListItemDto
    {
        public Guid Id { get; set; }

        public string HotelName { get; set; }
        public string City { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }

}
