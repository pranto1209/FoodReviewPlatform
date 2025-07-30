namespace FoodReviewPlatform.Utilities.Providers
{
    public interface IAppConfiguration
    {
        public string DatabaseConnectionString { get; }
        public string JwtKey { get; }
        public string JwtIssuer { get; }
        public string JwtAudience { get; }
    }
}
