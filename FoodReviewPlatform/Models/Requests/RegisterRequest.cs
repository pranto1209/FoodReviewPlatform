using System.ComponentModel.DataAnnotations;

namespace FoodReviewPlatform.Models.Requests
{
    public class RegisterRequest: BaseUserRequest
    {
        [Required]
        public string UserName { get; set; }
    }
}
