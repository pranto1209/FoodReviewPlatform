using FoodReviewPlatform.Database;
using FoodReviewPlatform.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly FoodReviewPlatformDbContext context;

        public LocationsController(FoodReviewPlatformDbContext context)
        {
            this.context = context;
        }

        [HttpGet("get-locations")]
        public async Task<IActionResult> GetLocations()
        {
            var query = await context.Locations.ToListAsync();

            return Ok(query);
        }

        [HttpGet("get-restaurants-by-location")]
        public async Task<IActionResult> GetRestaurantsByLocation([FromQuery] long id)
        {
            var query = await (from l in context.Locations.Where(l => l.Id == id)
                               join r in context.Restaurants on l.Id equals r.LocationId
                               orderby r.Name
                               select new RestaurantResponse
                               {
                                   Id = r.Id,
                                   Name = r.Name,
                                   Area = l.Area
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
