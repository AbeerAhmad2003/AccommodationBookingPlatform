using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Owners.Commands.DeleteOwner
{
    public record DeleteOwnerCommand(Guid Id) : IRequest<bool>;


}
