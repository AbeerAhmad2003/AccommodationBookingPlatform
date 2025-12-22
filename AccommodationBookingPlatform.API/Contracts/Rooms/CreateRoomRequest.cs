namespace AccommodationBookingPlatform.API.Contracts.Rooms
{
    public class CreateRoomRequest
    {
        public Guid RoomClassId { get; set; }
        public string Number { get; set; }
    }

}
