using AccommodationBookingPlatform.Application.Features.Hotels.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelById
{
    public record GetHotelByIdQuery(Guid Id) : IRequest<HotelDto>;
}
