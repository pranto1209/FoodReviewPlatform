namespace FoodReviewPlatform.Database.Entities;

public partial class CheckIn
{
    public long Id { get; set; }

    public DateTime CheckInTime { get; set; }

    public long UserId { get; set; }

    public long LocationId { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
