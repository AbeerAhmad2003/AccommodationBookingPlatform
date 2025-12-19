
using AccommodationBookingPlatform.Application;
using AccommodationBookingPlatform.Infrastructure;
using AccommodationBookingPlatform.Persistence;

namespace AccommodationBookingPlatform.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // *************************************
            // Register Layers
            // *************************************
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // *************************************
            // Controllers
            // *************************************
            builder.Services.AddControllers();

            // *************************************
            // Swagger
            // *************************************
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // *************************************
            // HttpContext Accessor (for CurrentUserService)
            // *************************************
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // *************************************
            // Swagger UI
            // *************************************
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json",
                        "Accommodation Booking API v1");

                    // يجعل Swagger على الرابط الرئيسي مباشرةً
                    c.RoutePrefix = string.Empty;
                });
            }

            // *************************************
            // Middlewares
            // *************************************
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            // *************************************
            // Controllers Mapping
            // *************************************
            app.MapControllers();

            app.Run();
        }
    }
}
