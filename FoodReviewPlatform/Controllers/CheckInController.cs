using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CheckInController(
        FoodReviewPlatformDbContext context, 
        IHttpContextAccessor httpContextAccessor,
        ICheckInService checkInService) : ControllerBase
    {
        [HttpGet("get-user-check-ins-by-restaurant")]
        public async Task<IActionResult> GetUserCheckInByRestaurant([FromQuery] long id)
        {
            var response = await checkInService.GetUserCheckInByRestaurant(id);

            return Ok(response);
        }

        [HttpGet("get-check-ins-by-user")]
        public async Task<IActionResult> GetCheckInsByUser()
        {
            //var userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

            var checkIns = await (from checkIn in context.CheckIns
                                  join restaurant in context.Restaurants on checkIn.RestaurantId equals restaurant.Id
                                  join location in context.Locations on restaurant.LocationId equals location.Id
                                  join user in context.Users on checkIn.UserId equals user.Id
                                  where user.Id == userId
                                  orderby checkIn.CheckInTime descending
                                  select new CheckInResponse
                                  {
                                      Id = checkIn.Id,
                                      UserName = user.UserName,
                                      RestaurantName = restaurant.Name,
                                      Area = location.Area,
                                      CheckInTime = checkIn.CheckInTime
                                  })
                                  .ToListAsync();

            return Ok(checkIns);
        }

        [HttpPost("add-check-in")]
        public async Task<IActionResult> AddCheckIn(AddCheckInRequest request)
        {
            //var userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

            var today = DateTime.UtcNow.Date;

            var existingCheckIn = await context.CheckIns
                .Where(c => c.UserId == userId && c.RestaurantId == request.RestaurantId && c.CheckInTime.Date == today)
                .FirstOrDefaultAsync();

            if (existingCheckIn != null)
            {
                return BadRequest("You have already checked in this restaurant today");
            }

            var checkIn = new CheckIn
            {
                UserId = userId,
                RestaurantId = request.RestaurantId,
                CheckInTime = DateTime.UtcNow
            };

            context.CheckIns.Add(checkIn);
            await context.SaveChangesAsync();

            return Ok();
        }

        //[Authorize]
        [HttpDelete("delete-check-in/{id}")]
        public async Task<IActionResult> DeleteCheckIn([FromRoute] long id)
        {
            await checkInService.DeleteCheckIn(id);

            return Ok();
        }
    }
}
