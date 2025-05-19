using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;

namespace FoodReviewPlatform.Services.Interface
{
    public interface IRestaurantService
    {
        Task<PaginatedData<RestaurantResponse>> GetRestaurantsByLocation(long id, FilteringRequest request);
        Task<Restaurant> GetRestaurantById(long id);
        Task AddRestaurant(AddRestaurantRequest request);
        Task UpdateRestaurant(UpdateRestaurantRequest request);
        Task DeleteRestaurant(long id);
    }
}
