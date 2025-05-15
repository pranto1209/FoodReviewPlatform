using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Services.Interface
{
    public interface ICheckInService
    {
        Task<IEnumerable<CheckInResponse>> GetUserCheckInByRestaurant(long restaurantId);
        Task DeleteCheckIn(long id);
    }
}
