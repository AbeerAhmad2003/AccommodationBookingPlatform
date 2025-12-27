using AccommodationBookingPlatform.Application.Common.Pagination;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Queries.GetAllBookings
{
    public record GetAdminBookingsQuery(int PageNumber = 1, int PageSize = 10)
     : IRequest<PaginatedList<AdminBookingListItemDto>>;

}
