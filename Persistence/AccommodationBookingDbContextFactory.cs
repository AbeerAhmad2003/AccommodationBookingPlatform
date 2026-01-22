using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AccommodationBookingPlatform.Persistence
{
    public class AccommodationBookingDbContextFactory
         : IDesignTimeDbContextFactory<AccommodationBookingDbContext>
    {
        public AccommodationBookingDbContext CreateDbContext(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AccommodationBookingPlatform.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder =
                new DbContextOptionsBuilder<AccommodationBookingDbContext>();

            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString(
                    "AccommodationBookingConnectionString"));

            return new AccommodationBookingDbContext(optionsBuilder.Options);
        }
    }
}
