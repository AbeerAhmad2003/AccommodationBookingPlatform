using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetBookingDetails
{
    public record GetBookingDetailsQuery(Guid BookingId)
     : IRequest<BookingDetailsDto>;

}
