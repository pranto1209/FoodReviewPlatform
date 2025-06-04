using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;
using FoodReviewPlatform.Repositories.Interfaces;
using FoodReviewPlatform.Services.Interfaces;
using FoodReviewPlatform.Utilities.Middlewares.ExceptionMiddlewares;

namespace FoodReviewPlatform.Services.Implementations
{
    public class LocationService(ILocationRepository locationRepository) : ILocationService
    {
        public async Task<PaginatedData<LocationReposne>> GetLocations(FilteringRequest request)
        {
            return await locationRepository.GetLocations(request);
        }

        public async Task<PaginatedData<LocationReposne>> GetNearbyLocations(double latitude, double longitude, FilteringRequest request)
        {
            return await locationRepository.GetNearbyLocations(latitude, longitude, request);
        }

        public async Task<LocationReposne> GetLocationById(long id)
        {
            var location = await locationRepository.GetLocationById(id);

            var response = new LocationReposne
            {
                Id = id,
                Name = location.Name,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            return response;
        }

        public async Task AddLocation(AddLocationRequest request)
        {
            var location = new Location
            {
                Name = request.Name,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            await locationRepository.AddLocation(location);
        }

        public async Task EditLocation(EditLocationRequest request)
        {
            var location = await locationRepository.GetLocationById(request.Id);

            if (location == null)
            {
                throw new CustomException("Location not found");
            }

            location.Name = request.Name;
            location.Latitude = request.Latitude;
            location.Longitude = request.Longitude;

            await locationRepository.EditLocation(location);
        }

        public async Task DeleteLocation(long id)
        {
            var location = await locationRepository.GetLocationById(id);

            if (location == null)
            {
                throw new CustomException("Location not found");
            }

            await locationRepository.DeleteLocation(location);
        }
    }
}
