using AccommodationBookingPlatform.API.Extensions;
using AccommodationBookingPlatform.API.Middleware;
using AccommodationBookingPlatform.Application;
using AccommodationBookingPlatform.Infrastructure;
using AccommodationBookingPlatform.Persistence;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;

namespace AccommodationBookingPlatform.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Layers
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // JWT
            builder.Services.AddJwtAuthentication(builder.Configuration);

            // Controllers
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AccommodationBookingAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Enter: Bearer {your JWT token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Swagger (خليه يشتغل على طول عشان ما يختفي بأي بيئة)
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Accommodation Booking API v1");
                c.RoutePrefix = string.Empty; // يعني يفتح على /
            });

            // Middlewares
            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            QuestPDF.Settings.License = LicenseType.Community;

            app.Run();
        }
    }
}
