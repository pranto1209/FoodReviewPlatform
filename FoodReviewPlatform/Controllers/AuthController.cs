using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser(LoginRequest request)
        {
            var response = await authService.LoginUser(request);
            return Ok(response);
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(RegisterRequest request)
        {
            await authService.RegisterUser(request);
            return Ok();
        }
    }
}
