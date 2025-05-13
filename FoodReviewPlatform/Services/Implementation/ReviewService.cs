using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Services.Implementation
{
    public class ReviewService(FoodReviewPlatformDbContext context) : IReviewService
    {
        public async Task<IEnumerable<ReviewResponse>> GetReviews(long locationId)
        {
            var reviews = await (from r in context.Reviews
                                 join u in context.Users on r.UserId equals u.Id
                                 where r.LocationId == locationId
                                 orderby r.ModificationTime.HasValue descending, r.ModificationTime descending, r.InsertionTime descending
                                 select new ReviewResponse
                                 {
                                     UserName = u.UserName,
                                     Rating = r.Rating,
                                     Comment = r.Comment,
                                     InsertionTime = r.InsertionTime,
                                     ModificationTime = r.ModificationTime
                                 })
                                 .ToListAsync();

            return reviews;
        }

        public async Task CreateReview(CreateReviewRequest request)
        {
            //var userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

            var existingReview = await context.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.LocationId == request.LocationId);

            if (existingReview != null)
            {
                throw new Exception("You have already reviewed this location");
            }

            var review = new Review
            {
                UserId = userId,
                LocationId = request.LocationId,
                Rating = request.Rating,
                Comment = request.Comment,
                InsertionTime = DateTime.UtcNow
            };

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
        }

        public async Task UpdateReview(UpdateReviewRequest request)
        {
            //var userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

            var review = await context.Reviews.FirstOrDefaultAsync(r => r.Id == request.Id && r.UserId == userId);

            if (review == null)
            {
                throw new Exception("Review not found");
            }

            review.Rating = request.Rating;
            review.Comment = request.Comment;
            review.InsertionTime = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }

        public async Task DeleteReview(long id)
        {
            //var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

            var review = await context.Reviews.FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (review == null)
            {
                throw new Exception("Review not found");
            }

            context.Reviews.Remove(review);
            await context.SaveChangesAsync();
        }
    }
}
