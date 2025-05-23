namespace FoodReviewPlatform.Utilities.Providers
{
    public class AppConfiguration(IConfiguration configuration) : IAppConfiguration
    {
        public string DatabaseConnectionString => configuration["ConnectionStrings:DefaultConnection"];
        public string JwtKey => configuration["Jwt:Key"];
        public string JwtIssuer => configuration["Jwt:Issuer"];
        public string JwtAudience => configuration["Jwt:Audience"];
    }
}
