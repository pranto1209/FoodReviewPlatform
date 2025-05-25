using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            var response = await authService.LoginUser(request);
            return Ok(response);
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request)
        {
            await authService.RegisterUser(request);
            return Ok();
        }

        [Authorize]
        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUserById()
        {
            var response = await authService.GetUserById();
            return Ok(response);
        }

        [Authorize]
        [HttpPut("edit-user")]
        public async Task<IActionResult> EditUser([FromBody] EditUserRequest request)
        {
            await authService.EditUser(request);
            return Ok();
        }

        [Authorize]
        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest request)
        {
            await authService.DeleteUser(request);
            return Ok();
        }
    }
}
