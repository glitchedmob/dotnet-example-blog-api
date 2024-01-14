using System.Linq.Expressions;
using System.Reflection;
using ExampleBlogApi.Entities;
using ExampleBlogApi.Entities.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlogApi.Database;

public sealed class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
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

                var method = typeof(AppDbContext)
                    .GetMethod(nameof(BuildSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Instance)!
                    .MakeGenericMethod(entityType.ClrType);
                var filter = method.Invoke(this, null);
                builder.Entity(entityType.ClrType).HasQueryFilter((dynamic)filter!);

            }
        }



        builder.ApplyConfiguration(new Post.Configuration());
        builder.ApplyConfiguration(new Comment.Configuration());
    }

    private Expression<Func<T, bool>> BuildSoftDeleteFilter<T>() where T : class, ISoftDelete
    {
        return entity => entity.DeletedAt == null;
    }
}
