using AccommodationBookingPlatform.Application.Features.Discounts.Common;
using AccommodationBookingPlatform.Application.Features.Discounts.Queries.GetActiveDiscounts;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;

namespace AccommodationBookingPlatform.Application.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {

            CreateMap<Discount, ActiveDiscountDto>();
            CreateMap<Discount, DiscountDto>();

        }
    }

}
