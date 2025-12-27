using AccommodationBookingPlatform.Application.Common.Pagination;
using AccommodationBookingPlatform.Domain.Common;
using AccommodationBookingPlatform.Domain.Entities;

namespace AccommodationBookingPlatform.Application.Contracts.Persistence
{
    public interface ICityRepository : IRepository<City>
    {
        Task<IReadOnlyList<City>> GetMostVisitedAsync(int count, CancellationToken cancellationToken = default);
        Task<PaginatedList<City>> GetCitiesAsync(Query<City> query, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<int> GetHotelsCountAsync(Guid cityId, CancellationToken cancellationToken = default);







    }
}
