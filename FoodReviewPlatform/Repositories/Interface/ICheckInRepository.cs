using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Repositories.Interface
{
    public interface ICheckInRepository
    {
        Task<PaginatedData<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId, FilteringRequest request);
        Task<PaginatedData<CheckInResponse>> GetCheckInsByUser(FilteringRequest request);
        Task<bool> GetTodaysCheckIn(AddCheckInRequest request);
        Task<CheckIn> GetCheckInById(long id);
        Task AddCheckIn(CheckIn request);
        Task DeleteCheckIn(CheckIn request);
    }
}
