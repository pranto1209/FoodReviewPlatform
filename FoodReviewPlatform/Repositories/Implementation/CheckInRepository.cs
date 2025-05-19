using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Repositories.Interface;
using FoodReviewPlatform.Utilities.Audit;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Repositories.Implementation
{
    public class CheckInRepository(FoodReviewPlatformDbContext context) : ICheckInRepository
    {
        public async Task<PaginatedData<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId, FilteringRequest request)
        {
            var query = from checkIn in context.CheckIns
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
                        };

            var response = new PaginatedData<CheckInResponse>
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

        public async Task<PaginatedData<CheckInResponse>> GetCheckInsByUser(FilteringRequest request)
        {
            var query = from checkIn in context.CheckIns
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
                        };

            var response = new PaginatedData<CheckInResponse>
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

        public async Task<bool> GetTodaysCheckIn(AddCheckInRequest request)
        {
            var existingCheckIn = await context.CheckIns
                .Where(c => c.UserId == AuditContext.UserId &&
                            c.RestaurantId == request.RestaurantId &&
                            c.CheckInTime.AddHours(6).Date == DateTime.UtcNow.AddHours(6).Date)
                .AnyAsync();

            return existingCheckIn;
        }

        public async Task<CheckIn> GetCheckInById(long id)
        {
            return await context.CheckIns.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCheckIn(CheckIn request)
        {
            await context.CheckIns.AddAsync(request);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCheckIn(CheckIn request)
        {
            context.CheckIns.Remove(request);
            await context.SaveChangesAsync();
        }
    }
}
