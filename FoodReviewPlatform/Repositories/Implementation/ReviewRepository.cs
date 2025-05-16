using FoodReviewPlatform.Database;
using FoodReviewPlatform.Repositories.Interface;

namespace FoodReviewPlatform.Repositories.Implementation
{
    public class ReviewRepository(FoodReviewPlatformDbContext context) : IReviewRepository
    {
    }
}
