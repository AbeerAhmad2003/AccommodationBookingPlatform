using AccommodationBookingPlatform.Application.Features.Owners.DTOs;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Owners.Commands.CreateOwner
{
    public record CreateOwnerCommand(
      string FirstName,
      string LastName,
      string Email,
      string PhoneNumber
  ) : IRequest<OwnerDto>;
}
