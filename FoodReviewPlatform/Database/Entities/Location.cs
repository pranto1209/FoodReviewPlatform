namespace FoodReviewPlatform.Database.Entities;

public partial class Location
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Area { get; set; } = null!;

    public string Category { get; set; } = null!;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public double? AverageRating { get; set; }

    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
