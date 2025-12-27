using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        Task<IReadOnlyList<Amenity>> GetByRoomClassIdAsync(
            Guid roomClassId,
            CancellationToken ct = default);

        Task<IReadOnlyList<Amenity>> GetAllAsync(CancellationToken ct = default);
    }
}
