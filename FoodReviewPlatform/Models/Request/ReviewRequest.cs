namespace FoodReviewPlatform.Models.Request
{
    public class CreateReviewRequest
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public long LocationId { get; set; }
    }

    public class UpdateReviewRequest
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
