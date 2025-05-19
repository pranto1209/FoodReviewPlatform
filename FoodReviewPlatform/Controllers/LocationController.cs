using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Services.Implementation;
using FoodReviewPlatform.Services.Interface;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("get-nearby-locations")]
        public async Task<IActionResult> GetNearbyLocations([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] FilteringRequest request)
        {
            var response = await locationService.GetNearbyLocations(latitude, longitude, request);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-location-by-id")]
        public async Task<IActionResult> GetLocationById([FromQuery] long id)
        {
            var response = await locationService.GetLocationById(id);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-location")]
        public async Task<IActionResult> AddLocation([FromBody] AddLocationRequest request)
        {
            await locationService.AddLocation(request);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-location")]
        public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationRequest request)
        {
            await locationService.UpdateLocation(request);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-location/{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] long id)
        {
            await locationService.DeleteLocation(id);
            return Ok();
        }
    }
}
