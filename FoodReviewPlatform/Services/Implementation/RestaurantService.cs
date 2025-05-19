using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Repositories.Interface;
using FoodReviewPlatform.Services.Interface;
using FoodReviewPlatform.Utilities.Exceptions;

namespace FoodReviewPlatform.Services.Implementation
{
    public class RestaurantService(IRestaurantRepository restaurantRepository) : IRestaurantService
    {
        public async Task<PaginatedData<RestaurantResponse>> GetRestaurantsByLocation(long id, FilteringRequest request)
        {
            return await restaurantRepository.GetRestaurantsByLocation(id, request);
        }

        public async Task<Restaurant> GetRestaurantById(long id)
        {
            return await restaurantRepository.GetRestaurantById(id);
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
