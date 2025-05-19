using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Services.Interface
{
    public interface ICheckInService
    {
        Task<PaginatedData<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId, FilteringRequest request);
        Task<PaginatedData<CheckInResponse>> GetCheckInsByUser(FilteringRequest request);
        Task AddCheckIn(AddCheckInRequest request);
        Task DeleteCheckIn(long id);
    }
}
