using AccommodationBookingPlatform.Domain.Common.Enums;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Common
{
    public class RoomClassInputDto
    {
        public Guid HotelId { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }

        public decimal PricePerNight { get; set; }
        public RoomType RoomType { get; set; }
    }
}
