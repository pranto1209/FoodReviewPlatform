namespace FoodReviewPlatform.Databases.Entities;

public partial class Location
{
    public long Id { get; set; }

    public string Area { get; set; } = null!;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
