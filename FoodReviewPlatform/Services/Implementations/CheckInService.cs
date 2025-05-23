using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;
using FoodReviewPlatform.Repositories.Interfaces;
using FoodReviewPlatform.Services.Interfaces;
using FoodReviewPlatform.Utilities.AuditInfos;
using FoodReviewPlatform.Utilities.Exceptions;

namespace FoodReviewPlatform.Services.Implementations
{
    public class CheckInService(ICheckInRepository checkInRepository) : ICheckInService
    {
        public async Task<PaginatedData<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId, FilteringRequest request)
        {
            return await checkInRepository.GetUserCheckInByRestaurant(restaurantId, request);
        }

        public async Task<PaginatedData<CheckInResponse>> GetCheckInsByUser(FilteringRequest request)
        {
            return await checkInRepository.GetCheckInsByUser(request);
        }

        public async Task AddCheckIn(AddCheckInRequest request)
        {
            var todaysCheckIn = await checkInRepository.GetTodaysCheckIn(request);

            if (todaysCheckIn == true)
            {
                throw new CustomException("You have already checked in this restaurant today");
            }

            var checkIn = new CheckIn
            {
                UserId = AuditContext.UserId,
                RestaurantId = request.RestaurantId,
                CheckInTime = DateTime.UtcNow
            };

            await checkInRepository.AddCheckIn(checkIn);
        }

        public async Task DeleteCheckIn(long id)
        {
            var checkIn = await checkInRepository.GetCheckInById(id);

            if (checkIn == null)
            {
                throw new CustomException("Invalid Check In");
            }

            await checkInRepository.DeleteCheckIn(checkIn);
        }
    }
}
