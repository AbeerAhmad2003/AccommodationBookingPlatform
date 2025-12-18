using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> CreateAsync(
        Booking booking,
        CancellationToken ct = default);

        Task<Booking?> GetByIdWithDetailsAsync(
            Guid bookingId,
            CancellationToken ct = default);

        Task<IReadOnlyList<Booking>> GetByUserIdAsync(
            Guid userId,
            CancellationToken ct = default);

        Task<PaginatedList<Booking>> GetBookingsAsync(
            Query<Booking> query,
            CancellationToken ct = default);

        Task DeleteAsync(
            Guid bookingId,
            CancellationToken ct = default);
        Task<bool> IsHotelAvailableAsync(
        Guid hotelId,
        DateTime from,
        DateTime to,
       int requestedRooms,
       CancellationToken ct = default);

    }
}
