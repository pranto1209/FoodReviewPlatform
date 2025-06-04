using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        [HttpGet("get-reviews-by-restaurant")]
        public async Task<IActionResult> GetReviewsByRestaurant([FromQuery] long id, [FromQuery] FilteringRequest request)
        {
            var response = await reviewService.GetReviewsByRestaurant(id, request);
            return Ok(response);
        }

        [HttpGet("get-average-rating-by-restaurant")]
        public async Task<IActionResult> GetAverageRatingByRestaurant([FromQuery] long id)
        {
            var response = await reviewService.GetAverageRatingByRestaurant(id);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("get-user-reviews-by-restaurant")]
        public async Task<IActionResult> GetUserReviewsByRestaurant([FromQuery] long id, [FromQuery] FilteringRequest request)
        {
            var response = await reviewService.GetUserReviewsByRestaurant(id, request);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("get-review-by-id")]
        public async Task<IActionResult> GetReviewById([FromQuery] long id)
        {
            var response = await reviewService.GetReviewById(id);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("get-reviews-by-user")]
        public async Task<IActionResult> GetReviewsByUser([FromQuery] FilteringRequest request)
        {
            var response = await reviewService.GetReviewsByUser(request);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request)
        {
            await reviewService.AddReview(request);
            return Ok();
        }

        [Authorize]
        [HttpPut("edit-review")]
        public async Task<IActionResult> EditReview([FromBody] EditReviewRequest request)
        {
            await reviewService.EditReview(request);
            return Ok();
        }

        [Authorize]
        [HttpDelete("delete-review/{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] long id)
        {
            await reviewService.DeleteReview(id);
            return Ok();
        }
    }
}
