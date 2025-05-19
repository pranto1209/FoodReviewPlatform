namespace FoodReviewPlatform.Models.Request
{
    public class UpdateReviewRequest
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
