namespace FoodReviewPlatform.Models.Response
{
    public class CheckInResponse
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string RestaurantName { get; set; }
        public string Area { get; set; }
        public DateTime CheckInTime { get; set; }
    }
}
