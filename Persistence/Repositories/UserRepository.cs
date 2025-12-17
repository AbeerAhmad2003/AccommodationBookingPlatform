using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccommodationBookingPlatform.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AccommodationBookingDbContext context)
            : base(context) { }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}
