﻿namespace FoodReviewPlatform.Database.Entities;

public partial class Review
{
    public long Id { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime ReviewTime { get; set; }

    public long UserId { get; set; }

    public long RestaurantId { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;
}
