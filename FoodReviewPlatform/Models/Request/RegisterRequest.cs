﻿using System.ComponentModel.DataAnnotations;

namespace FoodReviewPlatform.Models.Request
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
