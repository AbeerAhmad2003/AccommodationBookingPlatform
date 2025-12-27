using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using AccommodationBookingPlatform.Domain.Entities;
using AutoMapper;

namespace AccommodationBookingPlatform.Application.Profiles
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            // Entity → DTO
            CreateMap<Owner, OwnerDto>();



        }
    }
}

