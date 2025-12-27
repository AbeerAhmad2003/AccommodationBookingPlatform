using AccommodationBookingPlatform.API.Contracts.Auth;
using AccommodationBookingPlatform.Application.Features.Auth.Commands.Login;
using AccommodationBookingPlatform.Application.Features.Auth.Commands.Register;
using AutoMapper;

namespace AccommodationBookingPlatform.API.MappingProfiles
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegisterRequest, RegisterCommand>();
            CreateMap<LoginRequest, LoginCommand>();
        }
    }
}
