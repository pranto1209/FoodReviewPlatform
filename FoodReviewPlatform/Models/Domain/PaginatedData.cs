namespace FoodReviewPlatform.Models.Domain
{
    public class PaginatedData<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public long Total { get; set; }
        public List<T> Data { get; set; }
    }
}
