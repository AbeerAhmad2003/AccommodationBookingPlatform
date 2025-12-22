using AccommodationBookingPlatform.API.Contracts.Rooms;
using AccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom;
using AccommodationBookingPlatform.Application.Features.Rooms.Commands.UpdateRoom;
using AutoMapper;

namespace AccommodationBookingPlatform.API.MappingProfiles
{
    public class RoomsMappingProfile : Profile
    {
        public RoomsMappingProfile()
        {
            CreateMap<CreateRoomRequest, CreateRoomCommand>();
            CreateMap<UpdateRoomRequest, UpdateRoomCommand>();

        }
    }

}
