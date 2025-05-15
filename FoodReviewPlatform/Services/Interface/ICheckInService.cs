using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Services.Interface
{
    public interface ICheckInService
    {
        Task<IEnumerable<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId);
        Task<IEnumerable<CheckInResponse>> GetCheckInsByUser();
        Task AddCheckIn(AddCheckInRequest request);
        Task DeleteCheckIn(long id);
    }
}
