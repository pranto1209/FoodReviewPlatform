using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;

namespace FoodReviewPlatform.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<PaginatedData<RestaurantResponse>> GetRestaurantsByLocation(long id, FilteringRequest request);
        Task<RestaurantResponse> GetRestaurantById(long id);
        Task AddRestaurant(AddRestaurantRequest request);
        Task EditRestaurant(EditRestaurantRequest request);
        Task DeleteRestaurant(long id);
    }
}
