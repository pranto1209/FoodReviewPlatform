using System.ComponentModel.DataAnnotations;

namespace FoodReviewPlatform.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
