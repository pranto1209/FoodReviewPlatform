using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Services.Implementation
{
    public class CheckInService(
        FoodReviewPlatformDbContext context,
        IHttpContextAccessor httpContextAccessor) : ICheckInService
    {
        public async Task<IEnumerable<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId)
        {
            var userIdHeader = httpContextAccessor.HttpContext.Request.Headers["UserId"];

            if (!long.TryParse(userIdHeader, out var userId))
            {
                throw new InvalidOperationException("Invalid UserId in header");
            }

            var reviews = await (from checkIn in context.CheckIns
                                 join restaurant in context.Restaurants on checkIn.RestaurantId equals restaurant.Id
                                 join location in context.Locations on restaurant.LocationId equals location.Id
                                 join user in context.Users on checkIn.UserId equals user.Id
                                 where user.Id == userId && checkIn.RestaurantId == restaurantId
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

            return reviews;
        }

        public async Task<IEnumerable<CheckInResponse>> GetCheckInsByUser()
        {
            var userIdHeader = httpContextAccessor.HttpContext.Request.Headers["UserId"];

            if (!long.TryParse(userIdHeader, out var userId))
            {
                throw new InvalidOperationException("Invalid UserId in header");
            }

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

            return checkIns;
        }

        public async Task AddCheckIn(AddCheckInRequest request)
        {
            var userIdHeader = httpContextAccessor.HttpContext.Request.Headers["UserId"];

            if (!long.TryParse(userIdHeader, out var userId))
            {
                throw new InvalidOperationException("Invalid UserId in header");
            }

            var today = DateTime.UtcNow.Date;

            var existingCheckIn = await context.CheckIns
                .Where(c => c.UserId == userId && c.RestaurantId == request.RestaurantId && c.CheckInTime.Date == today)
                .FirstOrDefaultAsync();

            if (existingCheckIn != null)
            {
                throw new Exception("You have already checked in this restaurant today");
            }

            var checkIn = new CheckIn
            {
                UserId = userId,
                RestaurantId = request.RestaurantId,
                CheckInTime = DateTime.UtcNow
            };

            context.CheckIns.Add(checkIn);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCheckIn(long id)
        {
            var userIdHeader = httpContextAccessor.HttpContext.Request.Headers["UserId"];

            if (!long.TryParse(userIdHeader, out var userId))
            {
                throw new InvalidOperationException("Invalid UserId in header");
            }

            var checkIn = await context.CheckIns.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (checkIn == null)
            {
                throw new Exception("Check In not found");
            }

            context.CheckIns.Remove(checkIn);
            await context.SaveChangesAsync();
        }
    }
}
