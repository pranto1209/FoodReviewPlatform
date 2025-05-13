using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Services.Interface
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task Register(RegisterRequest request);
        Task<LoginResponse> LoginUser(LoginRequest request);
        Task RegisterUser(RegisterRequest request);
    }
}
