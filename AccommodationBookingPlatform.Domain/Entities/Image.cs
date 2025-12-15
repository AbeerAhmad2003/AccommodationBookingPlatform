using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Common.Enums;

namespace AccommodationBookingPlatform.Domain.Entities
{
    public class Image : EntityBase
    {
        public string Url { get; set; }
        public ImageType Type { get; set; }

        public Guid? HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        public Guid? RoomClassId { get; set; }
        public RoomClass? RoomClass { get; set; }

        public Guid? CityId { get; set; }
        public City? City { get; set; }
    }
}
