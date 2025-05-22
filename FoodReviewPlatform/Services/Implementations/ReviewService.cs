using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;
using FoodReviewPlatform.Repositories.Interfaces;
using FoodReviewPlatform.Services.Interfaces;
using FoodReviewPlatform.Utilities.Audits;
using FoodReviewPlatform.Utilities.Exceptions;

namespace FoodReviewPlatform.Services.Implementations
{
    public class ReviewService(IReviewRepository reviewRepository) : IReviewService
    {
        public async Task<PaginatedData<ReviewResponse>> GetReviewsByRestaurant(long restaurantId, FilteringRequest request)
        {
            return await reviewRepository.GetReviewsByRestaurant(restaurantId, request);
        }

        public async Task<PaginatedData<ReviewResponse>> GetUserReviewsByRestaurant(long restaurantId, FilteringRequest request)
        {
            return await reviewRepository.GetUserReviewsByRestaurant(restaurantId, request);
        }

        public async Task<PaginatedData<ReviewResponse>> GetReviewsByUser(FilteringRequest request)
        {
            return await reviewRepository.GetReviewsByUser(request);
        }

        public async Task<ReviewResponse> GetReviewById(long id)
        {
            var review = await reviewRepository.GetReviewById(id);

            var response = new ReviewResponse
            {
                Id = id,
                Rating = review.Rating,
                Comment = review.Comment,
                ReviewTime = review.ReviewTime,
            };

            return response;
        }

        public async Task<double> GetAverageRatingByRestaurant(long restaurantId)
        {
            return await reviewRepository.GetAverageRatingByRestaurant(restaurantId);
        }

        public async Task AddReview(AddReviewRequest request)
        {
            var review = new Review
            {
                UserId = AuditContext.UserId,
                RestaurantId = request.RestaurantId,
                Rating = request.Rating,
                Comment = request.Comment,
                ReviewTime = DateTime.UtcNow
            };

            await reviewRepository.AddReview(review);
        }

        public async Task UpdateReview(UpdateReviewRequest request)
        {
            var review = await reviewRepository.GetReviewById(request.Id);

            if (review == null)
            {
                throw new CustomException("Review not found");
            }

            review.Rating = request.Rating;
            review.Comment = request.Comment;
            review.ReviewTime = DateTime.UtcNow;

            await reviewRepository.UpdateReview(review);
        }

        public async Task DeleteReview(long id)
        {
            var review = await reviewRepository.GetReviewById(id);

            if (review == null)
            {
                throw new CustomException("Review not found");
            }

            await reviewRepository.DeleteReview(review);
        }
    }
}
