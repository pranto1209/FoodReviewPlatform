namespace FoodReviewPlatform.Databases.Entities;

public partial class CheckIn
{
    public long Id { get; set; }

    public DateTime CheckInTime { get; set; }

    public long UserId { get; set; }

    public long RestaurantId { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;
}
