using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Repositories.Interface;
using FoodReviewPlatform.Services.Interface;
using FoodReviewPlatform.Utilities.Exceptions;

namespace FoodReviewPlatform.Services.Implementation
{
    public class LocationService(ILocationRepository locationRepository, FoodReviewPlatformDbContext context) : ILocationService
    {
        public async Task<PaginatedData<LocationReposne>> GetLocations(FilteringRequest request)
        {
            return await locationRepository.GetLocations(request);
        }

        public async Task<PaginatedData<LocationReposne>> GetNearbyLocations(double latitude, double longitude, FilteringRequest request)
        {
            return await locationRepository.GetNearbyLocations(latitude, longitude, request);
        }

        public async Task<PaginatedData<RestaurantResponse>> GetRestaurantsByLocation(long id, FilteringRequest request)
        {
            return await locationRepository.GetRestaurantsByLocation(id, request);
        }

        public async Task AddLocation(AddLocationRequest request)
        {
            var location = new Location
            {
                Area = request.Area,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            await locationRepository.AddLocation(location);
        }

        public async Task UpdateLocation(UpdateLocationRequest request)
        {
            var location = await locationRepository.GetLocationById(request.Id);

            if (location == null)
            {
                throw new CustomException("Location not found");
            }

            location.Area = request.Area;
            location.Latitude = request.Latitude;
            location.Longitude = request.Longitude;

            await locationRepository.UpdateLocation(location);
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
