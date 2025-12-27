using AccommodationBookingPlatform.API.Contracts.Cities;
using AccommodationBookingPlatform.Application.Features.Cities.Commands.CreateCity;
using AccommodationBookingPlatform.Application.Features.Cities.Commands.UpdateCity;
using AutoMapper;

namespace AccommodationBookingPlatform.API.MappingProfiles
{
    public class CitiesMappingProfile : Profile
    {
        public CitiesMappingProfile()
        {
            CreateMap<CreateCityRequest, CreateCityCommand>();
            CreateMap<UpdateCityRequest, UpdateCityCommand>();
        }
    }

}
