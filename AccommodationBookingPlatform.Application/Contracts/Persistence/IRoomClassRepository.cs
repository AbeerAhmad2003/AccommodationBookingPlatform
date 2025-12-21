using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IRoomClassRepository : IRepository<RoomClass>
    {
        Task<bool> ExistsByNameInHotelAsync(
            Guid hotelId,
            string name,
            CancellationToken ct = default);

        Task<IReadOnlyList<RoomClass>> GetByHotelIdAsync(
            Guid hotelId,
            CancellationToken ct = default);

        Task<RoomClass?> GetByIdWithDetailsAsync(
            Guid id,
            CancellationToken ct = default);
    }
}
