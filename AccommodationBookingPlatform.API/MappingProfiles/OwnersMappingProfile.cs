using AccommodationBookingPlatform.API.Contracts.Owners;
using AccommodationBookingPlatform.Application.Features.Owners.Commands.CreateOwner;
using AccommodationBookingPlatform.Application.Features.Owners.Commands.UpdateOwner;
using AutoMapper;

namespace AccommodationBookingPlatform.API.MappingProfiles
{
    public class OwnersMappingProfile : Profile
    {
        public OwnersMappingProfile()
        {
            CreateMap<CreateOwnerRequest, CreateOwnerCommand>();
            CreateMap<UpdateOwnerRequest, UpdateOwnerCommand>();
        }
    }

}
