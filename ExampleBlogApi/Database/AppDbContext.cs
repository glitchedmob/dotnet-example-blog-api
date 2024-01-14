using ExampleBlogApi.Entities;
using ExampleBlogApi.Entities.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlogApi.Database;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public bool IncludeSoftDeletedEntities { get; set; } = false;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);

        options.AddInterceptors(new SoftDeleteInterceptor());
        options.AddInterceptors(new TimeStampInterceptor());
    }

    public required DbSet<Post> Posts { get; set; }
    public required DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ITimeStamped).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType)
                    .Property(nameof(ITimeStamped.CreatedAt))
                    .IsRequired()
                    .HasDefaultValueSql("now()");

                builder.Entity(entityType.ClrType)
                    .Property(nameof(ITimeStamped.UpdatedAt))
                    .IsRequired()
                    .HasDefaultValueSql("now()");
            }

            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType)
                    .HasQueryFilter((ISoftDelete e) => IncludeSoftDeletedEntities || e.DeletedAt == null);
            }
        }

        builder.ApplyConfiguration(new Post.Configuration(this));
        builder.ApplyConfiguration(new Comment.Configuration(this));
    }
}
