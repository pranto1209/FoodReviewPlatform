using FoodReviewPlatform.Databases;
using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;
using FoodReviewPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Repositories.Implementations
{
    public class LocationRepository(FoodReviewPlatformDbContext context) : ILocationRepository
    {
        public async Task<PaginatedData<LocationReposne>> GetLocations(FilteringRequest request)
        {
            var query = from location in context.Locations
                        where (string.IsNullOrWhiteSpace(request.SearchText) || location.Name.ToLower().Contains(request.SearchText.ToLower()))
                        orderby location.Id
                        select new LocationReposne
                        {
                            Id = location.Id,
                            Name = location.Name,
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
                            Name = location.Name,
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

        public async Task<Location> GetLocationById(long id)
        {
            return await context.Locations.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddLocation(Location location)
        {
            await context.Locations.AddAsync(location);
            await context.SaveChangesAsync();
        }

        public async Task EditLocation(Location location)
        {
            context.Locations.Update(location);
            await context.SaveChangesAsync();
        }

        public async Task DeleteLocation(Location location)
        {
            context.Locations.Remove(location);
            await context.SaveChangesAsync();
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
