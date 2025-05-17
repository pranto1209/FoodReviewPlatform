using System.Net;
using System.Text.Json;

namespace FoodReviewPlatform.Utilities.ExceptionHandlers
{
    public class DevelopmentExceptionMiddleware(RequestDelegate next, ILogger<DevelopmentExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = JsonSerializer.Serialize(new
            {
                Message = exception.ToString()
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsync(response);
        }
    }
}
