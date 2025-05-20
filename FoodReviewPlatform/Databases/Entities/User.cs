namespace FoodReviewPlatform.Databases.Entities;

public partial class User
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? EmailConfirmed { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public bool? PhoneNumberConfirmed { get; set; }

    public bool? TwoFactorEnabled { get; set; }

    public DateTime InsertionTime { get; set; }

    public DateTime? ModificationTime { get; set; }
}
