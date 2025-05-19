using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController(ICheckInService checkInService) : ControllerBase
    {
        [Authorize]
        [HttpGet("get-user-check-ins-by-restaurant")]
        public async Task<IActionResult> GetUserCheckInByRestaurant([FromQuery] long id, [FromQuery] FilteringRequest request)
        {
            var response = await checkInService.GetUserCheckInByRestaurant(id, request);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("get-check-ins-by-user")]
        public async Task<IActionResult> GetCheckInsByUser([FromQuery] FilteringRequest request)
        {
            var response = await checkInService.GetCheckInsByUser(request);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("add-check-in")]
        public async Task<IActionResult> AddCheckIn(AddCheckInRequest request)
        {
            await checkInService.AddCheckIn(request);
            return Ok();
        }

        [Authorize]
        [HttpDelete("delete-check-in/{id}")]
        public async Task<IActionResult> DeleteCheckIn([FromRoute] long id)
        {
            await checkInService.DeleteCheckIn(id);
            return Ok();
        }
    }
}
