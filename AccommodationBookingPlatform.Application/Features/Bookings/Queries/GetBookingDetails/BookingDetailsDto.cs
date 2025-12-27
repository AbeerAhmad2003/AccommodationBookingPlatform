namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetBookingDetails
{
    public class BookingDetailsDto
    {
        public Guid Id { get; set; }

        public string HotelName { get; set; }
        public string City { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int RoomsCount { get; set; }
        public decimal TotalPrice { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public InvoiceDto? Invoice { get; set; }
        public Guid RoomClassId { get; set; }
    }

}
