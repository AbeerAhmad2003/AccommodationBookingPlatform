using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Owners.Commands.UpdateOwner
{
    public record UpdateOwnerCommand(
     Guid Id,
     string? FirstName,
     string? LastName,
     string? PhoneNumber
 ) : IRequest<OwnerDto>;

}
