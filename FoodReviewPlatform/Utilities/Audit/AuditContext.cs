namespace FoodReviewPlatform.Utilities.Audit
{
    public static class AuditContext
    {
        private static readonly AsyncLocal<AuditInfo> auditInfo = new AsyncLocal<AuditInfo>();

        private static AuditInfo Current
        {
            get
            {
                if (auditInfo.Value == null)
                {
                    auditInfo.Value = new AuditInfo();
                }
                return auditInfo.Value;
            }
            set => auditInfo.Value = value;
        }

        public static long UserId
        {
            get => Current.UserId;
            set => Current.UserId = value;
        }

        public static string BearerToken
        {
            get => Current.BearerToken;
            set => Current.BearerToken = value;
        }
    }
}
