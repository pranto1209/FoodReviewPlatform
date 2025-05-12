using FoodReviewPlatform.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly FoodReviewPlatformDbContext _context;

        public LocationsController(FoodReviewPlatformDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLocations([FromQuery] string area = null, [FromQuery] string category = null)
        {
            var query = _context.Locations.AsQueryable();

            if (!string.IsNullOrEmpty(area))
            {
                query = query.Where(l => l.Area == area);
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(l => l.Category == category);
            }

            var locations = await query.ToListAsync();
            return Ok(locations);
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbyLocations([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radiusKm = 5)
        {
            // This is a simplified version - for production, consider using spatial queries
            var locations = await _context.Locations
                .Where(l => l.Latitude != null && l.Longitude != null)
                .ToListAsync();

            var nearbyLocations = locations
                .Where(l => CalculateDistance(latitude, longitude, l.Latitude.Value, l.Longitude.Value) <= radiusKm)
                .ToList();

            return Ok(nearbyLocations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation(int id)
        {
            var location = await _context.Locations
                .Include(l => l.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            // Calculate average rating
            if (location.Reviews.Any())
            {
                location.AverageRating = location.Reviews.Average(r => r.Rating);
            }

            return Ok(location);
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
