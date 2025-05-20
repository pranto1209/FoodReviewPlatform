namespace FoodReviewPlatform.Models.Requests
{
    public class AddRestaurantRequest
    {
        public string Name { get; set; }
        public long LocationId { get; set; }
    }
}
