using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        Task<bool> ExistsAsync(Guid ownerId, CancellationToken ct = default);
        Task<PaginatedList<Owner>> GetOwnersAsync(
      Query<Owner> query,
      CancellationToken ct = default);
    }
}
