using AccommodationBookingPlatform.API.Extensions;
using AccommodationBookingPlatform.API.Middleware;
using AccommodationBookingPlatform.Application;
using AccommodationBookingPlatform.Infrastructure;
using AccommodationBookingPlatform.Persistence;
using Microsoft.OpenApi.Models;

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
            // JWT Authentication
            // *************************************
            builder.Services.AddJwtAuthentication(builder.Configuration);

            // *************************************
            // Controllers
            // *************************************
            builder.Services.AddControllers();

            // *************************************
            // Swagger
            // *************************************
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarRentalAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token with Bearer prefix",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new string[]{}
                }
            });
            });
            // *************************************
            // HttpContext Accessor
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

                    c.RoutePrefix = string.Empty;
                });

            }
            // *************************************
            // Middlewares
            // *************************************
            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

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
