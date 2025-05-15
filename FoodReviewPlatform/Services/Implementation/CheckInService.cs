using FoodReviewPlatform.Database;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Services.Implementation
{
    public class CheckInService(FoodReviewPlatformDbContext context) : ICheckInService
    {
        public async Task<IEnumerable<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId)
        {
            var userId = 1;

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

        public async Task DeleteCheckIn(long id)
        {
            //var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

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
