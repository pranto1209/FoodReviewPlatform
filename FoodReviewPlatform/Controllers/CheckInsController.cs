using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CheckInsController : ControllerBase
    {
        private readonly FoodReviewPlatformDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckInsController(FoodReviewPlatformDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn(CheckIn createCheckInDto)
        {
            //var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

            // Check if user already checked in today
            var today = DateTime.UtcNow.Date;
            var existingCheckIn = await _context.CheckIns
                .FirstOrDefaultAsync(c => c.UserId == userId &&
                                         c.LocationId == createCheckInDto.LocationId &&
                                         c.CheckInTime.Date == today);

            if (existingCheckIn != null)
            {
                return BadRequest("You have already checked in to this location today");
            }

            var checkIn = new CheckIn
            {
                UserId = userId,
                LocationId = createCheckInDto.LocationId,
                CheckInTime = DateTime.UtcNow
            };

            _context.CheckIns.Add(checkIn);
            await _context.SaveChangesAsync();

            return Ok(checkIn);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserCheckIns()
        {
            //var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

            var checkIns = await _context.CheckIns
                .Include(c => c.Location)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CheckInTime)
                .ToListAsync();

            return Ok(checkIns);
        }
    }
}
