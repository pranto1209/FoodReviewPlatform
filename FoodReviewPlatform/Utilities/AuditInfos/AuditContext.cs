namespace FoodReviewPlatform.Utilities.AuditInfos
{
    public static class AuditContext
    {
        private static readonly AsyncLocal<AuditInfo> auditInfo = new AsyncLocal<AuditInfo>();

        public static AuditInfo Current
        {
            get => auditInfo.Value ??= new AuditInfo();
            set => auditInfo.Value = value;
        }

        public static long UserId
        {
            get => Current.UserId;
            set => Current.UserId = value;
        }

        public static string Email
        {
            get => Current.Email;
            set => Current.Email = value;
        }

        public static string BearerToken
        {
            get => Current.BearerToken;
            set => Current.BearerToken = value;
        }
    }
}
