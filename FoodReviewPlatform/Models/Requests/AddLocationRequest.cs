namespace FoodReviewPlatform.Models.Requests
{
    public class AddLocationRequest
    {
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
