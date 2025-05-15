using FoodReviewPlatform.Database;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Services.Implementation
{
    public class LocationService(FoodReviewPlatformDbContext context) : ILocationService
    {
        public async Task<PaginatedData<LocationReposne>> GetLocations(FilteringRequest request)
        {
            var query = from location in context.Locations
                        where (string.IsNullOrEmpty(request.SearchText) || location.Area.ToLower().Contains(request.SearchText.ToLower()))
                        orderby location.Id
                        select new LocationReposne
                        {
                            Id = location.Id,
                            Area = location.Area,
                            Latitude = location.Latitude,
                            Longitude = location.Longitude
                        };

            var response = new PaginatedData<LocationReposne>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Total = await query.CountAsync(),
                Data = request.IsPaginated
                            ? await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync()
                            : await query.ToListAsync()
            };

            return response;
        }

        public async Task<PaginatedData<LocationReposne>> GetNearbyLocations(double latitude, double longitude, FilteringRequest request)
        {
            var query = from location in context.Locations.Where(l => l.Latitude != null && l.Longitude != null)
                        orderby CalculateDistance(latitude, longitude, location.Latitude.Value, location.Longitude.Value) ascending
                        select new LocationReposne
                        {
                            Id = location.Id,
                            Area = location.Area,
                            Latitude = location.Latitude,
                            Longitude = location.Longitude
                        };

            var response = new PaginatedData<LocationReposne>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Total = await query.CountAsync(),
                Data = request.IsPaginated
                            ? await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync()
                            : await query.ToListAsync()
            };

            return response;
        }

        public async Task<PaginatedData<RestaurantResponse>> GetRestaurantsByLocation(long id, FilteringRequest request)
        {
            var query = from location in context.Locations.Where(l => l.Id == id)
                        join restaurant in context.Restaurants on location.Id equals restaurant.LocationId
                        orderby restaurant.Name
                        select new RestaurantResponse
                        {
                            Id = restaurant.Id,
                            Name = restaurant.Name,
                            Area = location.Area
                        };

            var response = new PaginatedData<RestaurantResponse>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Total = await query.CountAsync(),
                Data = request.IsPaginated
                            ? await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync()
                            : await query.ToListAsync()
            };

            return response;
        }

        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371;
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
