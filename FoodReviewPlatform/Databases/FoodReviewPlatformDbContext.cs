using FoodReviewPlatform.Databases.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Databases;

public partial class FoodReviewPlatformDbContext : DbContext
{
    public FoodReviewPlatformDbContext()
    {
    }

    public FoodReviewPlatformDbContext(DbContextOptions<FoodReviewPlatformDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CheckIn> CheckIns { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_check_in");

            entity.ToTable("check_in");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CheckInTime).HasColumnName("check_in_time");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("fk_check_in_restaurant_restaurant_id");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_location");

            entity.ToTable("location");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('locations_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_restaurant");

            entity.ToTable("restaurant");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");

            entity.HasOne(d => d.Location).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("fk_restaurant_location_location_id");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_review");

            entity.ToTable("review");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.ReviewTime).HasColumnName("review_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("fk_review_restaurant_restaurant_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_role");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_user");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("email");
            entity.Property(e => e.EmailConfirmed).HasColumnName("email_confirmed");
            entity.Property(e => e.InsertionTime).HasColumnName("insertion_time");
            entity.Property(e => e.ModificationTime).HasColumnName("modification_time");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            entity.Property(e => e.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
            entity.Property(e => e.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            entity.Property(e => e.UserName)
                .HasMaxLength(256)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_user_role");

            entity.ToTable("user_role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_user_role_role_role_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_role_user_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
