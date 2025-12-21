namespace AccommodationBookingPlatform.Application.Features.Rooms.Common
{
    public class RoomDto
    {
        public Guid Id { get; set; }

        public Guid RoomClassId { get; set; }
        public string RoomClassName { get; set; }

        public string Number { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}
