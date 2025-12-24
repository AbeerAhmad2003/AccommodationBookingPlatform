using AccommodationBookingPlatform.API.Contracts.Bookings;
using AccommodationBookingPlatform.Application.Features.Bookings.Commands.CreateBooking;
using AccommodationBookingPlatform.Application.Features.Bookings.Commands.UpdateBooking;
using AutoMapper;

namespace AccommodationBookingPlatform.API.MappingProfiles
{
    public class BookingApiMappingProfile : Profile
    {
        public BookingApiMappingProfile()
        {
            // Create
            CreateMap<CreateBookingRequest, CreateBookingCommand>();

            // Update
            CreateMap<UpdateBookingRequest, UpdateBookingCommand>();

        }
    }
}
