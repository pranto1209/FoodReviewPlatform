using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<PaginatedData<RestaurantResponse>> GetRestaurantsByLocation(long id, FilteringRequest request);
        Task<Restaurant> GetRestaurantById(long id);
        Task AddRestaurant(Restaurant request);
        Task UpdateRestaurant(Restaurant request);
        Task DeleteRestaurant(Restaurant request);
    }
}
