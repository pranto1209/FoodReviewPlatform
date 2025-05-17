using FoodReviewPlatform.Utilities.Exceptions;
using System.Net;
using System.Text.Json;

namespace FoodReviewPlatform.Utilities.ExceptionHandlers
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string customMessage = "";

            if (exception.GetType() == typeof(CustomException))
            {
                customMessage = exception.Message;
            }
            else
            {
                if (context.Request.Method == "GET")
                {
                    customMessage = "Failed to read";
                }
                if (context.Request.Method == "POST")
                {
                    customMessage = "Failed to create";
                }
                else if (context.Request.Method == "PUT")
                {
                    customMessage = "Failed to update";
                }
                else if (context.Request.Method == "DELETE")
                {
                    customMessage = "Failed to delete";
                }
                else
                {
                    customMessage = "Failed operation";
                }
            }

            var response = JsonSerializer.Serialize(new
            {
                Message = customMessage
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsync(response);
        }
    }
}
