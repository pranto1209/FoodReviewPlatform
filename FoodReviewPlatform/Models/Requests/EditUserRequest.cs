using System.ComponentModel.DataAnnotations;

namespace FoodReviewPlatform.Models.Requests
{
    public class EditUserRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? NewPassword { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
    }
}
