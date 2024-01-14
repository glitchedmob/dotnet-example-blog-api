using ExampleBlogApi.Entities;
using ExampleBlogApi.Entities.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlogApi.Database;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public required DbSet<Post> Posts { get; set; }
    public required DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (!typeof(ITimeStamped).IsAssignableFrom(entityType.ClrType))
            {
                continue;
            }

            builder.Entity(entityType.ClrType)
                .Property(nameof(ITimeStamped.CreatedAt))
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder.Entity(entityType.ClrType)
                .Property(nameof(ITimeStamped.UpdatedAt))
                .IsRequired()
                .HasDefaultValueSql("now()");
        }
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is ITimeStamped && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            var auditableEntity = (ITimeStamped)entityEntry.Entity;
            auditableEntity.UpdatedAt = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                auditableEntity.CreatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }
}
