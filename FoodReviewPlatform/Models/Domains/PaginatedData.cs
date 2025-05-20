namespace FoodReviewPlatform.Models.Domains
{
    public class PaginatedData<T>
    {
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
        public long Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
