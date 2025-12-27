using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Commands.DeleteAmenity
{
    public record DeleteAmenityCommand(Guid Id) : IRequest;

}
