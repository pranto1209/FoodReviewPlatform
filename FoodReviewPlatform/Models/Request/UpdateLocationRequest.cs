namespace FoodReviewPlatform.Models.Request
{
    public class UpdateLocationRequest
    {
        public long Id { get; set; }
        public string Area { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
