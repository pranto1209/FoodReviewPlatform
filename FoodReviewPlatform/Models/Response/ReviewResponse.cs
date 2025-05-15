namespace FoodReviewPlatform.Models.Response
{
    public class ReviewResponse
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string RestaurantName { get; set; }
        public string Area { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewTime { get; set; }
    }
}
