using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetBookingDetails;
using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUserBookings;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;

namespace AccommodationBookingPlatform.Application.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDetailsDto>()
                .ForMember(d => d.HotelName,
                    opt => opt.MapFrom(s => s.Hotel.Name))
                .ForMember(d => d.City,
                    opt => opt.MapFrom(s => s.Hotel.City.Name))
                .ForMember(d => d.PaymentMethod,
                    opt => opt.MapFrom(s => s.PaymentMethod.ToString()));
            CreateMap<Booking, UserBookingListItemDto>()
           .ForMember(d => d.HotelName,
               opt => opt.MapFrom(s => s.Hotel.Name))
           .ForMember(d => d.City,
               opt => opt.MapFrom(s => s.Hotel.City.Name));
        }
    }
}
