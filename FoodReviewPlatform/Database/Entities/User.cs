using System;
using System.Collections.Generic;

namespace FoodReviewPlatform.Database.Entities;

public partial class User
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? PhoneNumberConfirmed { get; set; }

    public DateTime InsertionTime { get; set; }

    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
