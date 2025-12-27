namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetAllBookings
{
    public class AdminBookingListItemDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public string HotelName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid RoomClassId { get; set; }
    }

}
