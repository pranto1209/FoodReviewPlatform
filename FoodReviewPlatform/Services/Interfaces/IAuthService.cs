using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginUser(LoginRequest request);
        Task RegisterUser(RegisterRequest request);
    }
}
