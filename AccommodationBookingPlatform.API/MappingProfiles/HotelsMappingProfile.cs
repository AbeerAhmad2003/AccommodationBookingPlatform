using AccommodationBookingPlatform.API.Contracts.Hotels;
using AccommodationBookingPlatform.Application.Features.Hotels.Commands.CreateHotel;
using AccommodationBookingPlatform.Application.Features.Hotels.Commands.UpdateHotel;
using AutoMapper;

namespace AccommodationBookingPlatform.API.MappingProfiles
{
    public class HotelsMappingProfile : Profile
    {
        public HotelsMappingProfile()
        {
            CreateMap<CreateHotelRequest, CreateHotelCommand>();
            CreateMap<UpdateHotelRequest, UpdateHotelCommand>();
        }
    }

}
