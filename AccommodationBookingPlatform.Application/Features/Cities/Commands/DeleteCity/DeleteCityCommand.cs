using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Cities.Commands.DeleteCity
{
    public record DeleteCityCommand(Guid Id) : IRequest<Unit>;

}
