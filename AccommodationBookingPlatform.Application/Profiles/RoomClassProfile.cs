using AccommodationBookingPlatform.Application.Features.RoomClasses.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;

namespace AccommodationBookingPlatform.Application.Profiles
{
    public class RoomClassProfile : Profile
    {
        public RoomClassProfile()
        {
            CreateMap<RoomClass, RoomClassDto>()
                .ForMember(d => d.HotelName,
                    opt => opt.MapFrom(s =>
                        s.Hotel != null ? s.Hotel.Name : string.Empty))
                .ForMember(d => d.RoomType,
                    opt => opt.MapFrom(s => s.RoomType.ToString()))
                .ForMember(d => d.RoomsCount,
                    opt => opt.MapFrom(s =>
                        s.Rooms != null ? s.Rooms.Count : 0));
        }
    }
}
