using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUserBookings;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetPastBookings
{
    public record GetPastBookingsQuery
     : IRequest<IEnumerable<UserBookingListItemDto>>;

}
