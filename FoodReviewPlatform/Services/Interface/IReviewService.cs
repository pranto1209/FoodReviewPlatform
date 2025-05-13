using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace FoodReviewPlatform.Services.Interface
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponse>> GetReviews(long locationId);
        Task CreateReview(CreateReviewRequest request);
        Task UpdateReview(UpdateReviewRequest request);
        Task DeleteReview(long id);
    }
}
