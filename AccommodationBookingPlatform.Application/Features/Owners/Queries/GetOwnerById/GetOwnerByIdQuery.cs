using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Owners.Queries.GetOwnerById
{
    public record GetOwnerByIdQuery(Guid Id)
        : IRequest<OwnerDto>;
}
