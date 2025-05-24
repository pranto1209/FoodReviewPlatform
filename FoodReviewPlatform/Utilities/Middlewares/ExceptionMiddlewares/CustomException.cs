using System.Globalization;

namespace FoodReviewPlatform.Utilities.Middlewares.ExceptionMiddlewares
{
    public class CustomException : Exception
    {
        public CustomException() : base() { }

        public CustomException(string message) : base(message) { }

        public CustomException(string message, Exception ex) : base(message, ex) { }

        public CustomException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
