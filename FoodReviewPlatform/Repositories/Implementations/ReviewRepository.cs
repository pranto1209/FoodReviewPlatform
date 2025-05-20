using FoodReviewPlatform.Databases;
using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Responses;
using FoodReviewPlatform.Repositories.Interfaces;
using FoodReviewPlatform.Utilities.Audits;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Repositories.Implementations
{
    public class ReviewRepository(FoodReviewPlatformDbContext context) : IReviewRepository
    {
        public async Task<PaginatedData<ReviewResponse>> GetReviewsByRestaurant(long restaurantId, FilteringRequest request)
        {
            var query = from review in context.Reviews
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
                        };

            var response = new PaginatedData<ReviewResponse>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Total = await query.CountAsync(),
                Data = request.IsPaginated
                            ? await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync()
                            : await query.ToListAsync()
            };

            return response;
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

        public async Task<PaginatedData<ReviewResponse>> GetUserReviewsByRestaurant(long restaurantId, FilteringRequest request)
        {
            var query = from review in context.Reviews
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
                        };

            var response = new PaginatedData<ReviewResponse>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Total = await query.CountAsync(),
                Data = request.IsPaginated
                            ? await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync()
                            : await query.ToListAsync()
            };

            return response;
        }

        public async Task<Review> GetReviewById(long id)
        {
            return await context.Reviews.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<PaginatedData<ReviewResponse>> GetReviewsByUser(FilteringRequest request)
        {
            var query = from review in context.Reviews
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
                        };

            var response = new PaginatedData<ReviewResponse>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Total = await query.CountAsync(),
                Data = request.IsPaginated
                            ? await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync()
                            : await query.ToListAsync()
            };

            return response;
        }

        public async Task AddReview(Review request)
        {
            await context.Reviews.AddAsync(request);
            await context.SaveChangesAsync();
        }

        public async Task UpdateReview(Review request)
        {
            context.Reviews.Update(request);
            await context.SaveChangesAsync();
        }

        public async Task DeleteReview(Review request)
        {
            context.Reviews.Remove(request);
            await context.SaveChangesAsync();
        }
    }
}
