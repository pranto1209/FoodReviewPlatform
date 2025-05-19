using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Repositories.Interface;
using FoodReviewPlatform.Services.Interface;
using FoodReviewPlatform.Utilities.Audit;
using FoodReviewPlatform.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Services.Implementation
{
    public class ReviewService(IReviewRepository reviewRepository, FoodReviewPlatformDbContext context) : IReviewService
    {
        public async Task<PaginatedData<ReviewResponse>> GetReviewsByRestaurant(long restaurantId, FilteringRequest request)
        {
            return await reviewRepository.GetReviewsByRestaurant(restaurantId, request);
        }

        public async Task<double> GetAverageRatingByRestaurant(long restaurantId)
        {
            return await reviewRepository.GetAverageRatingByRestaurant(restaurantId);
        }

        public async Task<PaginatedData<ReviewResponse>> GetUserReviewsByRestaurant(long restaurantId, FilteringRequest request)
        {
            return await reviewRepository.GetUserReviewsByRestaurant(restaurantId, request);
        }

        public async Task<PaginatedData<ReviewResponse>> GetReviewsByUser(FilteringRequest request)
        {
            return await reviewRepository.GetReviewsByUser(request);
        }

        public async Task<Review> GetReviewById(long id)
        {
            return await reviewRepository.GetReviewById(id);
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
            var review = await context.Reviews.FirstOrDefaultAsync(r => r.Id == request.Id && r.UserId == AuditContext.UserId);

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
            var review = await context.Reviews.FirstOrDefaultAsync(r => r.Id == id && r.UserId == AuditContext.UserId);

            if (review == null)
            {
                throw new CustomException("Review not found");
            }

            await reviewRepository.DeleteReview(review);
        }
    }
}
