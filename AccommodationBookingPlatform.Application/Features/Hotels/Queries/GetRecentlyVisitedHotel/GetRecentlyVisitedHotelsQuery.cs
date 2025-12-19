using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetRecentlyVisitedHotel
{
    public class GetRecentlyVisitedHotelsQuery
    : IRequest<IEnumerable<RecentlyVisitedHotelDto>>
    {
        public int Count { get; init; } = 5;
    }
}
