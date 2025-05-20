using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<PaginatedData<LocationReposne>> GetLocations(FilteringRequest request);
        Task<PaginatedData<LocationReposne>> GetNearbyLocations(double latitude, double longitude, FilteringRequest request);
        Task<Location> GetLocationById(long id);
        Task AddLocation(Location request);
        Task UpdateLocation(Location request);
        Task DeleteLocation(Location request);
    }
}
