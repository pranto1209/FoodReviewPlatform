using System;
using System.Collections.Generic;

namespace FoodReviewPlatform.Database.Entities;

public partial class Restaurant
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long LocationId { get; set; }

    public virtual Location Location { get; set; } = null!;
}
