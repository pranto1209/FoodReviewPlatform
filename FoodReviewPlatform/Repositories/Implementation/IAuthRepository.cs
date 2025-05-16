using FoodReviewPlatform.Database;
using FoodReviewPlatform.Repositories.Interface;

namespace FoodReviewPlatform.Repositories.Implementation
{
    public class AuthRepository(FoodReviewPlatformDbContext context) : IAuthRepository
    {
    }
}
