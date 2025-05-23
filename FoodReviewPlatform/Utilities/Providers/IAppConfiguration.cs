namespace FoodReviewPlatform.Utilities.Providers
{
    public interface IAppConfiguration
    {
        string DatabaseConnectionString { get; }
        public string JwtKey { get; }
        public string JwtIssuer { get; }
        public string JwtAudience { get; }
    }
}
