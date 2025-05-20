using FoodReviewPlatform.Databases;
using FoodReviewPlatform.Repositories.Interfaces;

namespace FoodReviewPlatform.Repositories.Implementations
{
    public class AuthRepository(FoodReviewPlatformDbContext context) : IAuthRepository
    {
    }
}
