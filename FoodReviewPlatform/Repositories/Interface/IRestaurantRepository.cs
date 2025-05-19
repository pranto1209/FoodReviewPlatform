using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Repositories.Interface
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
