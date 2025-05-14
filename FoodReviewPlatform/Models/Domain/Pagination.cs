namespace FoodReviewPlatform.Models.Domain
{
    public class Pagination
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public bool IsPaginated { get; set; } = true;
    }
}
