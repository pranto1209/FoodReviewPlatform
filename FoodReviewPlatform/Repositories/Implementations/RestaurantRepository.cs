using FoodReviewPlatform.Databases;
using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;
using FoodReviewPlatform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Repositories.Implementations
{
    public class RestaurantRepository(FoodReviewPlatformDbContext context) : IRestaurantRepository
    {
        public async Task<PaginatedData<RestaurantResponse>> GetRestaurantsByLocation(long id, FilteringRequest request)
        {
            var query = from location in context.Locations.Where(l => l.Id == id)
                        join restaurant in context.Restaurants on location.Id equals restaurant.LocationId
                        where (location.Id == id &&
                               (string.IsNullOrWhiteSpace(request.SearchText) || restaurant.Name.ToLower().Contains(request.SearchText.ToLower())))
                        orderby restaurant.Name
                        select new RestaurantResponse
                        {
                            Id = restaurant.Id,
                            Name = restaurant.Name,
                            LocationName = location.Name
                        };

            var response = new PaginatedData<RestaurantResponse>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Total = await query.CountAsync(),
                Data = request.IsPaginated
                            ? await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync()
                            : await query.ToListAsync()
            };

            return response;
        }

        public async Task<Restaurant> GetRestaurantById(long id)
        {
            return await context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRestaurant(Restaurant request)
        {
            await context.Restaurants.AddAsync(request);
            await context.SaveChangesAsync();
        }

        public async Task UpdateRestaurant(Restaurant request)
        {
            context.Restaurants.Update(request);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRestaurant(Restaurant request)
        {
            context.Restaurants.Remove(request);
            await context.SaveChangesAsync();
        }
    }
}
