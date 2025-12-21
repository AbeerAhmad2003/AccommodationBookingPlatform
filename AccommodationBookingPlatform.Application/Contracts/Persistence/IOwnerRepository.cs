using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        Task<bool> ExistsAsync(Guid ownerId, CancellationToken ct = default);
    }
}
