using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Repositories.Interfaces
{
    public interface ICheckInRepository
    {
        Task<PaginatedData<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId, FilteringRequest request);
        Task<PaginatedData<CheckInResponse>> GetCheckInsByUser(FilteringRequest request);
        Task<CheckIn> GetCheckInById(long id);
        Task<bool> GetTodaysCheckIn(AddCheckInRequest request);
        Task AddCheckIn(CheckIn checkIn);
        Task DeleteCheckIn(CheckIn checkIn);
    }
}
