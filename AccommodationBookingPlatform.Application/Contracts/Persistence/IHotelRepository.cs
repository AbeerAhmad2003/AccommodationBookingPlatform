using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        // User
        Task<PaginatedList<Hotel>> SearchAsync(Query<Hotel> query, CancellationToken ct);
        Task<IEnumerable<Hotel>> GetFeaturedDealsAsync(int count, CancellationToken ct);
        Task<IEnumerable<Hotel>> GetRecentlyVisitedAsync(Guid userId, int count, CancellationToken ct);

        // Admin
        Task<PaginatedList<Hotel>> GetHotelsAsync(Query<Hotel> query, CancellationToken ct);
        Task<bool> ExistsByNameAsync(string name, Guid cityId, CancellationToken ct);
        Task<Hotel?> GetHotelDetailsAsync(Guid hotelId, CancellationToken ct);
        Task DeleteHotelAsync(Guid hotelId, CancellationToken ct);

        // Reviews
        Task UpdateReviewById(Guid hotelId, double newRating, CancellationToken ct);

        Task<bool> ExistsAtLocationAsync(Guid cityId, double longitude, double latitude, double tolerance = 0.0005,
        CancellationToken ct = default);
        Task<Hotel?> GetByIdWithRoomClassesAsync(Guid id, CancellationToken ct = default);


    }

}
