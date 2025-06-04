using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<PaginatedData<RestaurantResponse>> GetRestaurantsByLocation(long id, FilteringRequest request);
        Task<Restaurant> GetRestaurantById(long id);
        Task AddRestaurant(Restaurant restaurant);
        Task EditRestaurant(Restaurant restaurant);
        Task DeleteRestaurant(Restaurant restaurant);
    }
}
