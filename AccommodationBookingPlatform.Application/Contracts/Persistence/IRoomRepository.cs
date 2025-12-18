using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<int> GetTotalRoomsByHotelAsync(Guid hotelId, CancellationToken ct = default);
    }
}
