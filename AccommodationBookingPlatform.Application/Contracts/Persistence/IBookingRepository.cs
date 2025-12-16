using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetByUserIdAsync(Guid userId);

        Task<bool> IsRoomAvailableAsync(Guid roomClassId, DateTime from, DateTime to);
    }
}
