using FoodReviewPlatform.Database;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        [HttpGet("get-reviews-by-restaurant")]
        public async Task<IActionResult> GetReviewsByRestaurant([FromQuery] long id)
        {
            var response = await reviewService.GetReviewsByRestaurant(id);

            return Ok(response);
        }

        [HttpGet("get-user-reviews-by-restaurant")]
        public async Task<IActionResult> GetUserReviewByRestaurant([FromQuery] long id)
        {
            var response = await reviewService.GetUserReviewByRestaurant(id);

            return Ok(response);
        }

        [HttpGet("get-review-by-id")]
        public async Task<IActionResult> GetReviewsById([FromQuery] long id)
        {
            var response = await reviewService.GetReviewById(id);

            return Ok(response);
        }

        //[Authorize]
        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request)
        {
            await reviewService.AddReview(request);

            return Ok();
        }

        //[Authorize]
        [HttpPut("update-review")]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewRequest request)
        {
            await reviewService.UpdateReview(request);

            return Ok();
        }

        //[Authorize]
        [HttpDelete("delete-review/{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] long id)
        {
            await reviewService.DeleteReview(id);

            return Ok();
        }
    }
}
