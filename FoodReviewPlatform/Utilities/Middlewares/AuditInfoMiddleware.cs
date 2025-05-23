using FoodReviewPlatform.Utilities.AuditInfos;
using System.Security.Claims;

namespace FoodReviewPlatform.Utilities.Middlewares
{
    public class AuditInfoMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            AuditContext.UserId = GetCurrentUserId(context);
            AuditContext.Email = GetCurrentEmail(context);
            AuditContext.BearerToken = GetBearerToken(context);

            await next(context);
        }

        private static long GetCurrentUserId(HttpContext context)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrWhiteSpace(userId))
            {
                return long.Parse(userId);
            }

            return 0;
        }

        private static string GetCurrentEmail(HttpContext context)
        {
            var emailClaim = context.User.FindFirstValue(ClaimTypes.Email);

            if (!string.IsNullOrWhiteSpace(emailClaim))
            {
                return emailClaim;
            }

            return string.Empty;
        }

        private static string GetBearerToken(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                string authorization = context.Request.Headers["Authorization"];

                if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    return authorization.Substring("Bearer ".Length).Trim();
                }
            }

            return string.Empty;
        }
    }
}
