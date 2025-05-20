using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Services.Interfaces
{
    public interface ICheckInService
    {
        Task<PaginatedData<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId, FilteringRequest request);
        Task<PaginatedData<CheckInResponse>> GetCheckInsByUser(FilteringRequest request);
        Task AddCheckIn(AddCheckInRequest request);
        Task DeleteCheckIn(long id);
    }
}
