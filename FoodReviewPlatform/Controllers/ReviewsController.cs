using FoodReviewPlatform.Database;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController(IReviewService reviewService) : ControllerBase
    {
        [HttpGet("get-reviews")]
        public async Task<IActionResult> GetReviews([FromQuery] long locationId)
        {
            var response = await reviewService.GetReviews(locationId);

            return Ok(response);
        }

        //[Authorize]
        [HttpPost("create-review")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequest request)
        {
            await reviewService.CreateReview(request);

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
