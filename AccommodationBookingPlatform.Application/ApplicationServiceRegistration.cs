using AccommodationBookingPlatform.Application.Common.Behaviours;
using AccommodationBookingPlatform.Application.Contracts.Services.Pricing;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AccommodationBookingPlatform.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(
      this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(
                typeof(ApplicationServiceRegistration).Assembly);
            services.AddScoped<IFeaturedDealCalculator, FeaturedDealCalculator>();
            services.AddScoped<IBookingPricingService, BookingPricingService>();

            return services;
        }
    }
}
