namespace FoodReviewPlatform.Models.Request
{
    public class AddLocationRequest
    {
        public string Area { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
