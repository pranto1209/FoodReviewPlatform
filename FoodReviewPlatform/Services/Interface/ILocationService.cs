using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Services.Interface
{
    public interface ILocationService
    {
        Task<PaginatedData<LocationReposne>> GetLocations(FilteringRequest request);
        Task<PaginatedData<LocationReposne>> GetNearbyLocations(double latitude, double longitude, FilteringRequest request);
        Task<Location> GetLocationById(long id);
        Task AddLocation(AddLocationRequest request);
        Task UpdateLocation(UpdateLocationRequest request);
        Task DeleteLocation(long id);
    }
}
