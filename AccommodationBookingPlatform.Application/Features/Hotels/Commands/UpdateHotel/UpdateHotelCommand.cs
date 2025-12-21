using AccommodationBookingPlatform.Application.Features.Hotels.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Commands.UpdateHotel
{
    public record UpdateHotelCommand(
        Guid Id,
        string Name,
        Guid CityId,
        Guid OwnerId,
        string PhoneNumber,
        double Longitude,
        double Latitude,
        string? BriefDescription,
        string? Description
    ) : IRequest<HotelDto>;
}
