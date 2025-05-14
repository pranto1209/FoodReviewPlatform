namespace FoodReviewPlatform.Models.Request
{
    public class AddReviewRequest
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public long RestaurantId { get; set; }
    }

    public class UpdateReviewRequest
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
