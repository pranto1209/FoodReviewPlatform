dotnet ef migrations add FoodReviewPlatformMigration --context FoodReviewPlatformDbContext --output-dir Database/Migrations

dotnet ef database update --context FoodReviewPlatformDbContext

dotnet ef migrations add AuthMigration --context AuthDbContext --output-dir Database/Migrations

dotnet ef database update --context AuthDbContext

dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=FoodReviewPlatformDB;User Id=postgres;Password=1209;" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Database/Entities --context-dir Database --context FoodReviewPlatformDbContext --force
