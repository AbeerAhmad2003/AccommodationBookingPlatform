namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Common
{
    public class RoomClassDto
    {
        public Guid Id { get; set; }

        public Guid HotelId { get; set; }
        public string HotelName { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }

        public decimal PricePerNight { get; set; }
        public string RoomType { get; set; }

        public int RoomsCount { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }

}
