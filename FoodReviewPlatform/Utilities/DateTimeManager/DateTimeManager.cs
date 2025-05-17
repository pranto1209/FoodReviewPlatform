namespace FoodReviewPlatform.Utilities.DateTimeManager
{
    public static class DateTimeManager
    {
        public static DateTime ConvertIntoLocalTime(this DateTime dateTime, string timeZoneId = "Asia/Dhaka")
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc), timeZone);
        }

        public static DateTime ConvertIntoUtcTime(this DateTime dateTime, string timeZoneId = "Asia/Dhaka")
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified), timeZone);
        }
    }
}
