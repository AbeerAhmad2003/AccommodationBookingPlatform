using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<PaginatedList<Hotel>> SearchAsync(Query<Hotel> query, CancellationToken cancellationToken = default);
        Task<IEnumerable<Hotel>> GetFeaturedDealsAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<Hotel>> GetRecentlyVisitedAsync(Guid userId, int count, CancellationToken cancellationToken = default);
        Task UpdateReviewById(Guid id, double newRating, CancellationToken cancellationToken = default);
    }
}
