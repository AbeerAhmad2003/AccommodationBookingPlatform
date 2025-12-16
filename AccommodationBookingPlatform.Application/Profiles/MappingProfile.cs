using AccommodationBookingPlatform.Application.Features.Cities.Queries.GetTrendingCities;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;

namespace AccommodationBookingPlatform.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, TrendingCityDto>()
            .ForMember(
                dest => dest.CityId,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.ThumbnailUrl,
                opt => opt.MapFrom(src =>
                    src.Thumbnail != null ? src.Thumbnail.Url : null)
            )
            .ForMember(
                dest => dest.BookingsCount,
                opt => opt.Ignore()
            );
        }

    }
}
