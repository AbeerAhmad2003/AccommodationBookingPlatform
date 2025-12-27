namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelDetails
{
    public class RoomClassDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }

        public decimal PricePerNight { get; set; }
        public string RoomType { get; set; }

        public List<string> Amenities { get; set; } = new();
        public List<string> Images { get; set; } = new();

        public bool IsAvailable { get; set; }
        public int TotalRooms { get; set; }
        public int BookedRooms { get; set; }
        public int AvailableRooms => TotalRooms - BookedRooms;
    }
}
