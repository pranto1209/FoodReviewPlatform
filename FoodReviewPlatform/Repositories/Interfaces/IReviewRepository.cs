using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<PaginatedData<ReviewResponse>> GetReviewsByRestaurant(long restaurantId, FilteringRequest request);
        Task<PaginatedData<ReviewResponse>> GetUserReviewsByRestaurant(long restaurantId, FilteringRequest request);
        Task<PaginatedData<ReviewResponse>> GetReviewsByUser(FilteringRequest request);
        Task<Review> GetReviewById(long id);
        Task<double> GetAverageRatingByRestaurant(long restaurantId);
        Task AddReview(Review review);
        Task EditReview(Review review);
        Task DeleteReview(Review review);
    }
}
