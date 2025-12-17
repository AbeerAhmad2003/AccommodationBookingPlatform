using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface ICityRepository : IRepository<City>
    {
        Task<IReadOnlyList<City>> GetMostVisitedAsync(int count, CancellationToken cancellationToken = default);
    }
}
