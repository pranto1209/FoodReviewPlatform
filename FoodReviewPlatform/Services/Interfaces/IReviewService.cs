using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Services.Interfaces
{
    public interface IReviewService
    {
        Task<PaginatedData<ReviewResponse>> GetReviewsByRestaurant(long restaurantId, FilteringRequest request);
        Task<PaginatedData<ReviewResponse>> GetUserReviewsByRestaurant(long restaurantId, FilteringRequest request);
        Task<PaginatedData<ReviewResponse>> GetReviewsByUser(FilteringRequest request);
        Task<ReviewResponse> GetReviewById(long id);
        Task<double> GetAverageRatingByRestaurant(long restaurantId);
        Task AddReview(AddReviewRequest request);
        Task UpdateReview(UpdateReviewRequest request);
        Task DeleteReview(long id);
    }
}
