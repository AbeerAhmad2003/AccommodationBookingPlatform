using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetFeaturedDeals;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetRecentlyVisitedHotel;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;

namespace AccommodationBookingPlatform.Application.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Hotel, FeaturedHotelDto>()
                .ForMember(d => d.HotelId,
                    o => o.MapFrom(s => s.Id))

                .ForMember(d => d.CityName,
                    o => o.MapFrom(s => s.City.Name))

                .ForMember(d => d.ThumbnailUrl,
                    o => o.MapFrom(s => s.Thumbnail != null ? s.Thumbnail.Url : null))
                .ForMember(d => d.OriginalPrice, o => o.Ignore())
                .ForMember(d => d.DiscountedPrice, o => o.Ignore());


            CreateMap<Booking, RecentlyVisitedHotelDto>()
                .ForMember(d => d.HotelId, o => o.MapFrom(s => s.HotelId))
                .ForMember(d => d.BookingId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Hotel.Name))
                .ForMember(d => d.CityName, o => o.MapFrom(s => s.Hotel.City.Name))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Hotel.City.Country))
                .ForMember(d => d.ReviewsRating, o => o.MapFrom(s => s.Hotel.ReviewsRating))
                .ForMember(d => d.CheckInDateUtc, o => o.MapFrom(s => s.CheckInDate))
                .ForMember(d => d.CheckOutDateUtc, o => o.MapFrom(s => s.CheckOutDate))
                .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.TotalPrice))
                .ForMember(d => d.ThumbnailUrl,
        o => o.MapFrom(s =>
            s.Hotel.Thumbnail != null ? s.Hotel.Thumbnail.Url : null));
        }
    }
}
