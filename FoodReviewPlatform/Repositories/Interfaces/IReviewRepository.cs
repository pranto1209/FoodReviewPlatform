using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Repositories.Interfaces
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
