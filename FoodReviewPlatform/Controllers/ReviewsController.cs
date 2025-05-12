using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly FoodReviewPlatformDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewsController(FoodReviewPlatformDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("location/{locationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLocationReviews(int locationId)
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Where(r => r.LocationId == locationId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(Review createReviewDto)
        {
            //var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.LocationId == createReviewDto.LocationId);

            if (existingReview != null)
            {
                return BadRequest("You have already reviewed this location");
            }

            var review = new Review
            {
                UserId = userId,
                LocationId = createReviewDto.LocationId,
                Rating = createReviewDto.Rating,
                Comment = createReviewDto.Comment
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, Review updateReviewDto)
        {
            //var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (review == null)
            {
                return NotFound();
            }

            review.Rating = updateReviewDto.Rating;
            review.Comment = updateReviewDto.Comment;
            review.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(review);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            //var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userId = 1;

            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
