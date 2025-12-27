using AccommodationBookingPlatform.API.Contracts.RoomClasses;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.CreateRoomClass;
using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.UpdateRoomClass;
using AutoMapper;

namespace AccommodationBookingPlatform.API.MappingProfiles
{
    public class RoomClassesMappingProfile : Profile
    {
        public RoomClassesMappingProfile()
        {
            CreateMap<CreateRoomClassRequest, CreateRoomClassCommand>();
            CreateMap<UpdateRoomClassRequest, UpdateRoomClassCommand>();

        }
    }
}
