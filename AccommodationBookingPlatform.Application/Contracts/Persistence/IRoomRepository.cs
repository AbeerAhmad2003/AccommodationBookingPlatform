using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<int> GetTotalRoomsByHotelAsync(
       Guid hotelId,
       CancellationToken ct = default);

        Task<bool> ExistsNumberInRoomClassAsync(
            Guid roomClassId,
            string number,
            CancellationToken ct = default);

        Task<IReadOnlyList<Room>> GetByRoomClassIdAsync(
            Guid roomClassId,
            CancellationToken ct = default);
    }
}
