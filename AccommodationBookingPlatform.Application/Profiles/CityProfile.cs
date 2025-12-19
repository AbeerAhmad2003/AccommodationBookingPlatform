using AccommodationBookingPlatform.Application.Features.Cities.Queries.GetTrendingCities;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;

namespace AccommodationBookingPlatform.Application.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, TrendingCityDto>()
                .ForMember(d => d.CityId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ThumbnailUrl,
                    o => o.MapFrom(s => s.Thumbnail != null ? s.Thumbnail.Url : null));
        }
    }

}
