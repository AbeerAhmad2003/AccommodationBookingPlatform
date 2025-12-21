using AccommodationBookingPlatform.Application.Features.Hotels.Common;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetFeaturedDeals;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelDetails;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetRecentlyVisitedHotel;
using AccommodationBookingPlatform.Application.Features.Hotels.Queries.SearchHotels;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;

namespace AccommodationBookingPlatform.Application.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            // ⭐ Featured Deals Mapping
            CreateMap<Hotel, FeaturedHotelDto>()
                .ForMember(d => d.HotelId,
                    o => o.MapFrom(s => s.Id))
                .ForMember(d => d.CityName,
                    o => o.MapFrom(s => s.City.Name))
                .ForMember(d => d.ThumbnailUrl,
                    o => o.MapFrom(s =>
                        s.Thumbnail != null ? s.Thumbnail.Url : null))
                .ForMember(d => d.OriginalPrice, o => o.Ignore())
                .ForMember(d => d.DiscountedPrice, o => o.Ignore());

            // ⭐ Recently Visited Hotels
            CreateMap<Booking, RecentlyVisitedHotelDto>()
                .ForMember(d => d.HotelId,
                    o => o.MapFrom(s => s.HotelId))
                .ForMember(d => d.BookingId,
                    o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name,
                    o => o.MapFrom(s => s.Hotel.Name))
                .ForMember(d => d.CityName,
                    o => o.MapFrom(s => s.Hotel.City.Name))
                .ForMember(d => d.Country,
                    o => o.MapFrom(s => s.Hotel.City.Country))
                .ForMember(d => d.ReviewsRating,
                    o => o.MapFrom(s => s.Hotel.ReviewsRating))
                .ForMember(d => d.CheckInDateUtc,
                    o => o.MapFrom(s => s.CheckInDate))
                .ForMember(d => d.CheckOutDateUtc,
                    o => o.MapFrom(s => s.CheckOutDate))
                .ForMember(d => d.TotalPrice,
                    o => o.MapFrom(s => s.TotalPrice))
                .ForMember(d => d.ThumbnailUrl,
                    o => o.MapFrom(s =>
                        s.Hotel.Thumbnail != null ? s.Hotel.Thumbnail.Url : null));

            // ⭐ Search Hotels Result
            CreateMap<Hotel, HotelSearchResultDto>()
                .ForMember(d => d.CityName,
                    o => o.MapFrom(s => s.City.Name))
                .ForMember(d => d.ThumbnailUrl,
                    o => o.MapFrom(s =>
                        s.Thumbnail != null ? s.Thumbnail.Url : string.Empty))
                .ForMember(d => d.PriceFrom,
                    o => o.MapFrom(s =>
                        s.RoomClasses.Any()
                            ? s.RoomClasses.Min(rc => rc.PricePerNight)
                            : 0))
                .ForMember(d => d.Rating,
                    o => o.MapFrom(s => s.ReviewsRating));
            // ⭐ Hotel Details
            CreateMap<Hotel, HotelDetailsDto>()
                .ForMember(d => d.CityName,
                    o => o.MapFrom(s => s.City.Name))
                .ForMember(d => d.Country,
                    o => o.MapFrom(s => s.City.Country))
                .ForMember(d => d.ThumbnailUrl,
                    o => o.MapFrom(s => s.Thumbnail != null ? s.Thumbnail.Url : null))
                .ForMember(d => d.GalleryUrls,
                    o => o.MapFrom(s => s.Gallery.Select(img => img.Url)))
                .ForMember(d => d.ReviewsCount,
                    o => o.MapFrom(s => s.ReviewsCount))
                .ForMember(d => d.ReviewsRating,
                    o => o.MapFrom(s => s.ReviewsRating))
                .ForMember(d => d.RoomClasses,
                    o => o.MapFrom(s => s.RoomClasses))
                .ForMember(d => d.Reviews,
                    o => o.MapFrom(s => s.Reviews));

            CreateMap<RoomClass, RoomClassDto>()
                .ForMember(d => d.RoomType,
                    o => o.MapFrom(s => s.RoomType.ToString()))
                .ForMember(d => d.Amenities,
                    o => o.MapFrom(s => s.Amenities.Select(a => a.Name)))
                .ForMember(d => d.Images,
                    o => o.MapFrom(s => s.Gallery.Select(img => img.Url)));

            CreateMap<Review, ReviewDto>()
                .ForMember(d => d.GuestName,
                    o => o.MapFrom(s => s.Guest.FirstName + " " + s.Guest.LastName))
                .ForMember(d => d.Rating,
                    o => o.MapFrom(s => (int)s.Rating));

            CreateMap<Hotel, HotelDto>()
          .ForMember(d => d.CityName,
              opt => opt.MapFrom(s => s.City != null ? s.City.Name : string.Empty))
          .ForMember(d => d.OwnerName,
              opt => opt.MapFrom(s =>
                  s.Owner != null
                      ? s.Owner.FirstName + " " + s.Owner.LastName
                      : string.Empty))
          .ForMember(d => d.RoomsCount,
              opt => opt.MapFrom(s =>
                  s.RoomClasses != null
                      ? s.RoomClasses.Sum(rc => rc.Rooms.Count)
                      : 0));

        }
    }
}
