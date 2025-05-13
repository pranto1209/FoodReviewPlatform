using FoodReviewPlatform.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Database;

public partial class AuthDbContext : IdentityDbContext
{
    public AuthDbContext()
    {
    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = UserRoleClass.User,
                    Name = UserRoleClass.User,
                    NormalizedName = UserRoleClass.User.ToUpper()
                }
            };

        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}
