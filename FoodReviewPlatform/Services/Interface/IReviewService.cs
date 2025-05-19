using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Services.Interface
{
    public interface IReviewService
    {
        Task<PaginatedData<ReviewResponse>> GetReviewsByRestaurant(long restaurantId, FilteringRequest request);
        Task<double> GetAverageRatingByRestaurant(long restaurantId);
        Task<PaginatedData<ReviewResponse>> GetUserReviewsByRestaurant(long restaurantId, FilteringRequest request);
        Task<ReviewResponse> GetReviewById(long id);
        Task<PaginatedData<ReviewResponse>> GetReviewsByUser(FilteringRequest request);
        Task AddReview(AddReviewRequest request);
        Task UpdateReview(UpdateReviewRequest request);
        Task DeleteReview(long id);
    }
}
