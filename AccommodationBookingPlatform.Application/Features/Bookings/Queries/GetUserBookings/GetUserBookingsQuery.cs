using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetUserBookings
{
    public record GetUserBookingsQuery
     : IRequest<IEnumerable<UserBookingListItemDto>>;

}
