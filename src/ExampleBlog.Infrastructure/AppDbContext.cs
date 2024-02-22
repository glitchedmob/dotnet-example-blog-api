using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.EntityConfiguration;
using ExampleBlog.Infrastructure.SoftDelete;
using ExampleBlog.Infrastructure.TimeStamped;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlog.Infrastructure;

public sealed class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);

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
                entityType.SetSoftDeleteQueryFilter();
            }
        }

        builder.ApplyConfiguration(new PostConfiguration());
        builder.ApplyConfiguration(new CommentConfiguration());
    }
}
