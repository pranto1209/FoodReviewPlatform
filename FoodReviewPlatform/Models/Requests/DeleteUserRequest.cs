using System.ComponentModel.DataAnnotations;

namespace FoodReviewPlatform.Models.Requests
{
    public class DeleteUserRequest
    {
        [Required]
        public string CurrentPassword { get; set; }
    }
}
