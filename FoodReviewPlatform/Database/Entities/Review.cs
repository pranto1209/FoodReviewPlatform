using System;
using System.Collections.Generic;

namespace FoodReviewPlatform.Database.Entities;

public partial class Review
{
    public long Id { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime InsertionTime { get; set; }

    public DateTime? ModificationTime { get; set; }

    public long UserId { get; set; }

    public long LocationId { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
