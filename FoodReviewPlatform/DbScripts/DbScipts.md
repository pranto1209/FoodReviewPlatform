dotnet ef migrations add InitialMigration --output-dir Database/Migrations
dotnet ef database update
--schema food_review_schema

dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=FoodReviewPlatformDB;User Id=postgres;Password=1209;" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Database/Entities --context-dir Database --context FoodReviewPlatformDbContext --force
