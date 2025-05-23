using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet("get-my-profile")]
        public IActionResult GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(new
            {
                UserId = userId,
                UserName = username,
                Email = email
            });
        }

    }
}
