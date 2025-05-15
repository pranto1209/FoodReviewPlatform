using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodReviewPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController(ILocationService locationService) : ControllerBase
    {
        [HttpGet("get-locations")]
        public async Task<IActionResult> GetLocations([FromQuery] FilteringRequest request)
        {
            var response = await locationService.GetLocations(request);
            return Ok(response);
        }

        [HttpGet("get-restaurants-by-location")]
        public async Task<IActionResult> GetRestaurantsByLocation([FromQuery] long id, [FromQuery] FilteringRequest request)
        {
            var response = await locationService.GetRestaurantsByLocation(id, request);
            return Ok(response);
        }

        [HttpGet("get-nearby-locations")]
        public async Task<IActionResult> GetNearbyLocations([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] FilteringRequest request)
        {
            var response = await locationService.GetNearbyLocations(latitude, longitude, request);
            return Ok(response);
        }
    }
}
