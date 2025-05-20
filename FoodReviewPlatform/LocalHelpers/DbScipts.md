# FoodReviewPlatform

### Add Migrations
dotnet ef migrations add FoodReviewPlatformMigration --context FoodReviewPlatformDbContext

### Update Database
dotnet ef database update --context FoodReviewPlatformDbContext

### Scaffold DbContext (PostgreSQL)
dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=FoodReviewPlatformDB;User Id=postgres;Password=1209;" Npgsql.EntityFrameworkCore.PostgreSQL --context FoodReviewPlatformDbContext --output-dir Databases/Entities --context-dir Databases --force

### Scaffold DbContext (SqlServer)
dotnet ef dbcontext scaffold "Server=localhost;Database=FoodReviewPlatformDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --context FoodReviewPlatformDbContext --output-dir Databases/Entities --context-dir Databases --force
