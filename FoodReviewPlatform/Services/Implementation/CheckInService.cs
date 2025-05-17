using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Services.Interface;
using FoodReviewPlatform.Utilities.Audit;
using FoodReviewPlatform.Utilities.DateTimeManager;
using FoodReviewPlatform.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Services.Implementation
{
    public class CheckInService(FoodReviewPlatformDbContext context) : ICheckInService
    {
        public async Task<IEnumerable<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId)
        {
            var reviews = await (from checkIn in context.CheckIns
                                 join restaurant in context.Restaurants on checkIn.RestaurantId equals restaurant.Id
                                 join location in context.Locations on restaurant.LocationId equals location.Id
                                 join user in context.Users on checkIn.UserId equals user.Id
                                 where user.Id == AuditContext.UserId && checkIn.RestaurantId == restaurantId
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
            var checkIns = await (from checkIn in context.CheckIns
                                  join restaurant in context.Restaurants on checkIn.RestaurantId equals restaurant.Id
                                  join location in context.Locations on restaurant.LocationId equals location.Id
                                  join user in context.Users on checkIn.UserId equals user.Id
                                  where user.Id == AuditContext.UserId
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
            var today = DateTime.UtcNow.ConvertIntoLocalTime();

            var existingCheckIn = await context.CheckIns
                .Where(c => c.UserId == AuditContext.UserId &&
                            c.RestaurantId == request.RestaurantId &&
                            c.CheckInTime.AddHours(6).Date == DateTime.UtcNow.AddHours(6).Date)
                .FirstOrDefaultAsync();

            if (existingCheckIn != null)
            {
                throw new CustomException("You have already checked in this restaurant today");
            }

            var checkIn = new CheckIn
            {
                UserId = AuditContext.UserId,
                RestaurantId = request.RestaurantId,
                CheckInTime = DateTime.UtcNow
            };

            await context.CheckIns.AddAsync(checkIn);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCheckIn(long id)
        {
            var checkIn = await context.CheckIns.FirstOrDefaultAsync(c => c.Id == id && c.UserId == AuditContext.UserId);

            if (checkIn == null)
            {
                throw new CustomException("Check In not found");
            }

            context.CheckIns.Remove(checkIn);
            await context.SaveChangesAsync();
        }
    }
}
