using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetAllBookings;
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

            CreateMap<Booking, UserBookingListItemDto>()
       .ForMember(d => d.HotelName,
           opt => opt.MapFrom(s => s.Hotel.Name))
       .ForMember(d => d.City,
           opt => opt.MapFrom(s => s.Hotel.City.Name))
       .ForMember(d => d.RoomClassId,
           opt => opt.MapFrom(s => s.RoomClassId));

            CreateMap<InvoiceRecord, InvoiceDto>();

            CreateMap<Booking, AdminBookingListItemDto>()
     .ForMember(d => d.UserEmail,
         opt => opt.MapFrom(s => s.User.Email))
     .ForMember(d => d.HotelName,
         opt => opt.MapFrom(s => s.Hotel.Name))
     .ForMember(d => d.CheckIn,
         opt => opt.MapFrom(s => s.CheckInDate))
     .ForMember(d => d.CheckOut,
         opt => opt.MapFrom(s => s.CheckOutDate))
     .ForMember(d => d.RoomClassId,
         opt => opt.MapFrom(s => s.RoomClassId));

            CreateMap<Booking, BookingDetailsDto>()
    .ForMember(d => d.HotelName,
        opt => opt.MapFrom(s => s.Hotel.Name))
    .ForMember(d => d.City,
        opt => opt.MapFrom(s => s.Hotel.City.Name))
    .ForMember(d => d.CheckInDate,
        opt => opt.MapFrom(s => s.CheckInDate))
    .ForMember(d => d.CheckOutDate,
        opt => opt.MapFrom(s => s.CheckOutDate))
    .ForMember(d => d.PaymentMethod,
        opt => opt.MapFrom(s => s.PaymentMethod.ToString()))
    .ForMember(d => d.Invoice,
        opt => opt.MapFrom(s => s.InvoiceRecords
            .OrderByDescending(i => i.CreatedAtUtc)
            .FirstOrDefault()))
    .ForMember(d => d.AllocatedRoomNumbers,
        opt => opt.MapFrom(s => s.BookingRooms
            .Select(br => br.Room.Number)
            .OrderBy(n => n)
            .ToList()));

        }
    }
}
