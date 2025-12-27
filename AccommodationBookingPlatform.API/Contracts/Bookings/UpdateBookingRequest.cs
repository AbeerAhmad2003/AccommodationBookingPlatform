using AccommodationBookingPlatform.Domain.Common.Enums;

namespace AccommodationBookingPlatform.API.Contracts.Bookings
{
    public class UpdateBookingRequest
    {
        public Guid RoomClassId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int Adults { get; set; }

        public int Children { get; set; }

        public int RoomsCount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }

}
