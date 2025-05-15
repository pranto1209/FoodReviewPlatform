namespace FoodReviewPlatform.Models.Domain
{
    public class FilteringRequest
    {
        public string? SearchText { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public bool IsPaginated { get; set; } = false;
    }
}
