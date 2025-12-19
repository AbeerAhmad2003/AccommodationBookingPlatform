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
        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
           => await _context.Users.AnyAsync(u => u.Id == id, cancellationToken);
        public async Task AddAsync(User user, CancellationToken ct)
        {
            await _context.Users.AddAsync(user, ct);
            await _context.SaveChangesAsync(ct);
        }
    }
}
