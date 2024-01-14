using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExampleBlogApi.Database.Extensions;

public static class DbContextExtensions
{
    public static EntityEntry<TEntity> ForceRemove<TEntity>(this AppDbContext context, TEntity entity)
        where TEntity : class
    {
        var entry = context.Remove(entity);

        entry.CurrentValues[SoftDeleteInterceptor.ForceDeleteFlag] = true;

        return entry;
    }

    public static void ForceRemoveRange<TEntity>(this AppDbContext context, IEnumerable<TEntity> entities)
        where TEntity : class
    {
        foreach (var entity in entities)
        {
            context.ForceRemove(entity);
        }
    }
}
