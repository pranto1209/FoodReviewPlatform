namespace FoodReviewPlatform.Utilities.Middlewares.AuditMiddlewares
{
    public class AuditInfo
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string BearerToken { get; set; }
    }
}
