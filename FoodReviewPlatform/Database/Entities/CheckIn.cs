namespace FoodReviewPlatform.Database.Entities;

public partial class CheckIn
{
    public long Id { get; set; }

    public DateTime CheckInTime { get; set; }

    public long UserId { get; set; }

    public long? LocationId { get; set; }

    public long? RestaurantId { get; set; }

    public virtual Location? Location { get; set; }

    public virtual Restaurant? Restaurant { get; set; }

    public virtual User User { get; set; } = null!;
}
