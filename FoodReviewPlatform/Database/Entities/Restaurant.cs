namespace FoodReviewPlatform.Database.Entities;

public partial class Restaurant
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long LocationId { get; set; }

    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
