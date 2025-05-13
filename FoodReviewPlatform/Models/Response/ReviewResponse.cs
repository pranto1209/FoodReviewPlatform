namespace FoodReviewPlatform.Models.Response
{
    public class ReviewResponse
    {
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime InsertionTime { get; set; }
        public DateTime? ModificationTime { get; set; }
    }
}
