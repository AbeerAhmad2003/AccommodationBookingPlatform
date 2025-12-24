using AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUserBookings;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUpcomingBookings
{
    public record GetUpcomingBookingsQuery
    : IRequest<IEnumerable<UserBookingListItemDto>>;

}
