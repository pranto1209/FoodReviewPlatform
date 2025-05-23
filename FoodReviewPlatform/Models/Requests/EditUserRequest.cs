namespace FoodReviewPlatform.Models.Requests
{
    public class EditUserRequest: BaseUserRequest
    {
        public string NewUserName { get; set; }
        public string NewEmail { get; set; }
        public string NewPassword { get; set; }
    }
}
