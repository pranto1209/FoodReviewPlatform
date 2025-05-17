using FoodReviewPlatform.Utilities.Audit;

namespace FoodReviewPlatform.Utilities.AuditHandlers
{
    public class AuditInfoMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            AuditContext.UserId = GetCurrentUserId(context);
            AuditContext.BearerToken = GetBearerToken(context);

            await next(context);
        }

        private static long GetCurrentUserId(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("UserId"))
            {
                string userId = context.Request.Headers["UserId"];

                if (string.IsNullOrEmpty(userId)) return 0;

                return long.Parse(userId);
            }

            return 0;
        }
        private static string GetBearerToken(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                string authHeader = context.Request.Headers["Authorization"];

                if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    return authHeader.Substring("Bearer ".Length).Trim();
                }
            }

            return string.Empty;
        }
    }
}
