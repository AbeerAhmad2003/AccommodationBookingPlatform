using AccommodationBookingPlatform.Application.Features.Cities.Common;
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

            CreateMap<City, CityDto>()
              .ForMember(d => d.HotelsCount,
                  opt => opt.MapFrom(src => src.Hotels.Count))
              .ForMember(d => d.ThumbnailUrl,
                  opt => opt.MapFrom(src =>
                      src.Thumbnail != null
                          ? src.Thumbnail.Url
                          : null));
        }
    }

}
