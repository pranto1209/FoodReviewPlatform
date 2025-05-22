using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;
using FoodReviewPlatform.Repositories.Interfaces;
using FoodReviewPlatform.Services.Interfaces;
using FoodReviewPlatform.Utilities.Exceptions;

namespace FoodReviewPlatform.Services.Implementations
{
    public class RestaurantService(IRestaurantRepository restaurantRepository) : IRestaurantService
    {
        public async Task<PaginatedData<RestaurantResponse>> GetRestaurantsByLocation(long id, FilteringRequest request)
        {
            return await restaurantRepository.GetRestaurantsByLocation(id, request);
        }

        public async Task<RestaurantResponse> GetRestaurantById(long id)
        {
            var restaurant = await restaurantRepository.GetRestaurantById(id);

            var response = new RestaurantResponse
            {
                Id = id,
                Name = restaurant.Name
            };

            return response;
        }

        public async Task AddRestaurant(AddRestaurantRequest request)
        {
            var restaurant = new Restaurant
            {
                Name = request.Name,
                LocationId = request.LocationId,
            };

            await restaurantRepository.AddRestaurant(restaurant);
        }

        public async Task UpdateRestaurant(UpdateRestaurantRequest request)
        {
            var restaurant = await restaurantRepository.GetRestaurantById(request.Id);

            if (restaurant == null)
            {
                throw new CustomException("Location not found");
            }

            restaurant.Name = request.Name;

            await restaurantRepository.UpdateRestaurant(restaurant);
        }

        public async Task DeleteRestaurant(long id)
        {
            var restaurant = await restaurantRepository.GetRestaurantById(id);

            if (restaurant == null)
            {
                throw new CustomException("Location not found");
            }

            await restaurantRepository.DeleteRestaurant(restaurant);
        }
    }
}
