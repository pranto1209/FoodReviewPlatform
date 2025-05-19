using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Repositories.Interface
{
    public interface IReviewRepository
    {
        Task<PaginatedData<ReviewResponse>> GetReviewsByRestaurant(long restaurantId, FilteringRequest request);
        Task<double> GetAverageRatingByRestaurant(long restaurantId);
        Task<PaginatedData<ReviewResponse>> GetUserReviewsByRestaurant(long restaurantId, FilteringRequest request);
        Task<PaginatedData<ReviewResponse>> GetReviewsByUser(FilteringRequest request);
        Task<Review> GetReviewById(long id);
        Task AddReview(Review request);
        Task UpdateReview(Review request);
        Task DeleteReview(Review request);
    }
}
