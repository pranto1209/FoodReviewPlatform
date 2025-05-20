namespace FoodReviewPlatform.Models.Requests
{
    public class UpdateReviewRequest
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
