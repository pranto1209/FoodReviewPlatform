namespace FoodReviewPlatform.Models.Responses
{
    public class ReviewResponse
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string RestaurantName { get; set; }
        public string LocationName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewTime { get; set; }
    }
}
