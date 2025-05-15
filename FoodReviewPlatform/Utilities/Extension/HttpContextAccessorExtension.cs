namespace FoodReviewPlatform.Utilities.Extension
{
    public static class HttpContextAccessorExtensions
    {
        public static long GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            var userIdHeader = httpContextAccessor.HttpContext.Request.Headers["UserId"];

            if (!long.TryParse(userIdHeader, out var userId))
            {
                throw new InvalidOperationException("Invalid UserId in header");
            }

            return userId;
        }
    }
}
