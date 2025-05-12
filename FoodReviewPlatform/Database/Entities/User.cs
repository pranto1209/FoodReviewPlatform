namespace FoodReviewPlatform.Database.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsEmailVerified { get; set; }

    public string? SocialProvider { get; set; }

    public string? SocialProviderId { get; set; }

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
