using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Services.Interface;
using FoodReviewPlatform.Utilities.Audit;
using FoodReviewPlatform.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Services.Implementation
{
    public class ReviewService(FoodReviewPlatformDbContext context) : IReviewService
    {
        public async Task<IEnumerable<ReviewResponse>> GetReviewsByRestaurant(long restaurantId)
        {
            var reviews = await (from review in context.Reviews
                                 join restaurant in context.Restaurants on review.RestaurantId equals restaurant.Id
                                 join location in context.Locations on restaurant.LocationId equals location.Id
                                 join user in context.Users on review.UserId equals user.Id
                                 where review.RestaurantId == restaurantId
                                 orderby review.ReviewTime descending
                                 select new ReviewResponse
                                 {
                                     Id = review.Id,
                                     UserName = user.UserName,
                                     RestaurantName = restaurant.Name,
                                     Area = location.Area,
                                     Rating = review.Rating,
                                     Comment = review.Comment,
                                     ReviewTime = review.ReviewTime
                                 })
                                 .ToListAsync();

            return reviews;
        }

        public async Task<double> GetAverageRatingByRestaurant(long restaurantId)
        {
            var reviews = await (from review in context.Reviews
                                 join restaurant in context.Restaurants on review.RestaurantId equals restaurant.Id
                                 where review.RestaurantId == restaurantId
                                 select review.Rating).ToListAsync();

            var averageRating = reviews.Count() > 0 ? reviews.Average() : 0.0;

            return averageRating;
        }

        public async Task<IEnumerable<ReviewResponse>> GetUserReviewsByRestaurant(long restaurantId)
        {
            var reviews = await (from review in context.Reviews
                                 join restaurant in context.Restaurants on review.RestaurantId equals restaurant.Id
                                 join location in context.Locations on restaurant.LocationId equals location.Id
                                 join user in context.Users on review.UserId equals user.Id
                                 where user.Id == AuditContext.UserId && review.RestaurantId == restaurantId
                                 orderby review.ReviewTime descending
                                 select new ReviewResponse
                                 {
                                     Id = review.Id,
                                     UserName = user.UserName,
                                     RestaurantName = restaurant.Name,
                                     Area = location.Area,
                                     Rating = review.Rating,
                                     Comment = review.Comment,
                                     ReviewTime = review.ReviewTime
                                 })
                                 .ToListAsync();

            return reviews;
        }

        public async Task<ReviewResponse> GetReviewById(long id)
        {
            var reviews = await (from review in context.Reviews
                                 join restaurant in context.Restaurants on review.RestaurantId equals restaurant.Id
                                 join location in context.Locations on restaurant.LocationId equals location.Id
                                 join user in context.Users on review.UserId equals user.Id
                                 where review.Id == id
                                 orderby review.ReviewTime descending
                                 select new ReviewResponse
                                 {
                                     Id = review.Id,
                                     UserName = user.UserName,
                                     RestaurantName = restaurant.Name,
                                     Area = location.Area,
                                     Rating = review.Rating,
                                     Comment = review.Comment,
                                     ReviewTime = review.ReviewTime
                                 })
                                 .FirstOrDefaultAsync();

            return reviews!;
        }

        public async Task<IEnumerable<ReviewResponse>> GetReviewsByUser()
        {
            var reviews = await (from review in context.Reviews
                                 join restaurant in context.Restaurants on review.RestaurantId equals restaurant.Id
                                 join location in context.Locations on restaurant.LocationId equals location.Id
                                 join user in context.Users on review.UserId equals user.Id
                                 where user.Id == AuditContext.UserId
                                 orderby review.ReviewTime descending
                                 select new ReviewResponse
                                 {
                                     Id = review.Id,
                                     UserName = user.UserName,
                                     RestaurantName = restaurant.Name,
                                     Area = location.Area,
                                     Rating = review.Rating,
                                     Comment = review.Comment,
                                     ReviewTime = review.ReviewTime
                                 })
                                 .ToListAsync();

            return reviews;
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

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
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

            await context.SaveChangesAsync();
        }

        public async Task DeleteReview(long id)
        {
            var review = await context.Reviews.FirstOrDefaultAsync(r => r.Id == id && r.UserId == AuditContext.UserId);

            if (review == null)
            {
                throw new CustomException("Review not found");
            }

            context.Reviews.Remove(review);
            await context.SaveChangesAsync();
        }
    }
}
