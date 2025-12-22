namespace AccommodationBookingPlatform.API.Contracts.Rooms
{
    public class UpdateRoomRequest
    {
        public Guid Id { get; set; }
        public Guid RoomClassId { get; set; }
        public string Number { get; set; }
    }
}
