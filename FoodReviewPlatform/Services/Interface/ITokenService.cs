using FoodReviewPlatform.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace FoodReviewPlatform.Services.Interface
{
    public interface ITokenService
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
        string CreateJwtToken(User user, List<string> roles);
    }
}
