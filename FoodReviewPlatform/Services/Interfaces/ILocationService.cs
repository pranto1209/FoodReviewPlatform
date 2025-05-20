using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Services.Interfaces
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
