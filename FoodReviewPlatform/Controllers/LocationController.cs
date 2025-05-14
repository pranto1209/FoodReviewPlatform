using FoodReviewPlatform.Database;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly FoodReviewPlatformDbContext context;

        public LocationController(FoodReviewPlatformDbContext context)
        {
            this.context = context;
        }

        [HttpGet("get-locations")]
        public async Task<IActionResult> GetLocations([FromQuery] Pagination request)
        {
            var query = from l in context.Locations
                        orderby l.Id
                        select new LocationReposne
                        {
                            Id = l.Id,
                            Area = l.Area,
                            Latitude = l.Latitude,
                            Longitude = l.Longitude
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

            return Ok(response);
        }

        [HttpGet("get-restaurants-by-location")]
        public async Task<IActionResult> GetRestaurantsByLocation([FromQuery] long id)
        {
            var query = await (from location in context.Locations.Where(l => l.Id == id)
                               join restaurant in context.Restaurants on location.Id equals restaurant.LocationId
                               orderby restaurant.Name
                               select new RestaurantResponse
                               {
                                   Id = restaurant.Id,
                                   Name = restaurant.Name,
                                   Area = location.Area
                               })
                               .ToListAsync();

            return Ok(query);
        }

        [HttpGet("get-nearby-locations")]
        public async Task<IActionResult> GetNearbyLocations([FromQuery] double latitude, [FromQuery] double longitude)
        {
            var query = await (from l in context.Locations.Where(l => l.Latitude != null && l.Longitude != null)
                               orderby CalculateDistance(latitude, longitude, l.Latitude.Value, l.Longitude.Value) ascending
                               select l)
                               .ToListAsync();

            return Ok(query);
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Haversine formula implementation
            var R = 6371; // Earth radius in km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
