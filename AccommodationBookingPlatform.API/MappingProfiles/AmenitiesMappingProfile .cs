using AccommodationBookingPlatform.API.Contracts.Amenities;
using AccommodationBookingPlatform.Application.Features.Amenities.Commands.CreateAmenity;
using AccommodationBookingPlatform.Application.Features.Amenities.Commands.UpdateAmenity;
using AutoMapper;

namespace AccommodationBookingPlatform.API.MappingProfiles
{
    public class AmenitiesMappingProfile : Profile
    {
        public AmenitiesMappingProfile()
        {
            CreateMap<CreateAmenityRequest, CreateAmenityCommand>();
            CreateMap<UpdateAmenityRequest, UpdateAmenityCommand>();
        }

    }
}
