using AccommodationBookingPlatform.Domain.Common.Enums;

namespace AccommodationBookingPlatform.API.Contracts.RoomClasses
{
    public class UpdateRoomClassRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }

        public decimal PricePerNight { get; set; }
        public RoomType RoomType { get; set; }
    }
}
