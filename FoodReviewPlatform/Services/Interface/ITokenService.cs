using FoodReviewPlatform.Database.Entities;

namespace FoodReviewPlatform.Services.Interface
{
    public interface ITokenService
    {
        string CreateJwtToken(User user, List<string> roles);
    }
}
