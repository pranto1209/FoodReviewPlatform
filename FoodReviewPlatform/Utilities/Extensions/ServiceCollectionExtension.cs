using FoodReviewPlatform.Database;
using FoodReviewPlatform.Repositories.Implementation;
using FoodReviewPlatform.Repositories.Interface;
using FoodReviewPlatform.Services.Implementation;
using FoodReviewPlatform.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Utilities.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FoodReviewPlatformDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICheckInService, CheckInService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IReviewService, ReviewService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICheckInRepository, CheckInRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}
