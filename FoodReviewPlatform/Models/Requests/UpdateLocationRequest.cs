namespace FoodReviewPlatform.Models.Requests
{
    public class UpdateLocationRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
