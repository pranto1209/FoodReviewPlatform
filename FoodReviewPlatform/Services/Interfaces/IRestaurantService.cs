using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Services.Interfaces
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
