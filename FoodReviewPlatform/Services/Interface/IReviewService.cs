using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Services.Interface
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponse>> GetReviewsByRestaurant(long restaurantId);
        Task<double> GetAverageRatingByRestaurant(long restaurantId);
        Task<IEnumerable<ReviewResponse>> GetUserReviewsByRestaurant(long restaurantId);
        Task<ReviewResponse> GetReviewById(long id);
        Task<IEnumerable<ReviewResponse>> GetReviewsByUser();
        Task AddReview(AddReviewRequest request);
        Task UpdateReview(UpdateReviewRequest request);
        Task DeleteReview(long id);
    }
}
