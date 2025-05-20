using FoodReviewPlatform.Databases;
using FoodReviewPlatform.Repositories.Implementations;
using FoodReviewPlatform.Repositories.Interfaces;
using FoodReviewPlatform.Services.Implementations;
using FoodReviewPlatform.Services.Interfaces;
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
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IReviewService, ReviewService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICheckInRepository, CheckInRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}
