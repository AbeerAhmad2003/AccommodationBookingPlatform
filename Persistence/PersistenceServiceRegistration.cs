using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccommodationBookingPlatform.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AccommodationBookingDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("AccommodationBookingConnectionString")));

            // Base Repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            // Core Repositories
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            // Supporting Repositories (إذا موجودة)
            //services.AddScoped<IImageRepository, ImageRepository>();
            // services.AddScoped<IReviewRepository, ReviewRepository>();
            // services.AddScoped<IOwnerRepository, OwnerRepository>();

            return services;
        }
    }

}
