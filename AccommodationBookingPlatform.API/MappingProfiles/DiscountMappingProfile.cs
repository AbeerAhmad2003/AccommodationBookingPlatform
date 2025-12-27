using AccommodationBookingPlatform.API.Contracts.Discounts;
using AccommodationBookingPlatform.Application.Features.Discounts.Commands.CreateDiscount;
using AccommodationBookingPlatform.Application.Features.Discounts.Commands.UpdateDiscount;
using AutoMapper;

namespace AccommodationBookingPlatform.API.MappingProfiles
{
    public class DiscountMappingProfile : Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<CreateDiscountRequest, CreateDiscountCommand>();

            CreateMap<UpdateDiscountRequest, UpdateDiscountCommand>();



        }
    }

}
