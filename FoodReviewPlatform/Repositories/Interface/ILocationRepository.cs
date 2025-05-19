using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Repositories.Interface
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
