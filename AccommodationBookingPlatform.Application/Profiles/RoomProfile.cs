using AccommodationBookingPlatform.Application.Features.Rooms.Common;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;

namespace AccommodationBookingPlatform.Application.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDto>()
                .ForMember(d => d.RoomClassName,
                    opt => opt.MapFrom(s =>
                        s.RoomClass != null ? s.RoomClass.Name : string.Empty));
        }
    }
}
