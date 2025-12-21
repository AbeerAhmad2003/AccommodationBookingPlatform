using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Commands.DeleteHotel
{
    public record DeleteHotelCommand(Guid Id) : IRequest;
}
