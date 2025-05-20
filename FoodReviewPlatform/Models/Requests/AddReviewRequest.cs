namespace FoodReviewPlatform.Models.Requests
{
    public class AddReviewRequest
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public long RestaurantId { get; set; }
    }
}
