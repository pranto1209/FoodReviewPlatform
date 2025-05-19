using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Repositories.Interface;
using FoodReviewPlatform.Services.Implementation;
using FoodReviewPlatform.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController(IRestaurantService restaurantService) : ControllerBase
    {
        [HttpGet("get-restaurants-by-location")]
        public async Task<IActionResult> GetRestaurantsByLocation([FromQuery] long id, [FromQuery] FilteringRequest request)
        {
            var response = await restaurantService.GetRestaurantsByLocation(id, request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-restaurant-by-id")]
        public async Task<IActionResult> GetRestaurantById([FromQuery] long id)
        {
            var response = await restaurantService.GetRestaurantById(id);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-restaurant")]
        public async Task<IActionResult> AddRestaurant([FromBody] AddRestaurantRequest request)
        {
            await restaurantService.AddRestaurant(request);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-restaurant")]
        public async Task<IActionResult> UpdateRestaurant([FromBody] UpdateRestaurantRequest request)
        {
            await restaurantService.UpdateRestaurant(request);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-restaurant/{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] long id)
        {
            await restaurantService.DeleteRestaurant(id);
            return Ok();
        }
    }
}
