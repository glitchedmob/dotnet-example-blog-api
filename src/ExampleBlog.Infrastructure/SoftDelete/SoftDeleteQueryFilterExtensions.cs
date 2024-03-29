using System.Linq.Expressions;
using System.Reflection;
using ExampleBlog.Core.Entities.Behaviors;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExampleBlog.Infrastructure.SoftDelete;

public static class SoftDeleteQueryFilterExtensions
{
    public static void SetSoftDeleteQueryFilter(this IMutableEntityType entityData)
    {
        var methodToCall = typeof(SoftDeleteQueryFilterExtensions)
            .GetMethod(nameof(GetCascadeSoftDeleteFilter),
                BindingFlags.NonPublic | BindingFlags.Static)!
            .MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall
            .Invoke(null, new object[] { });
        entityData.SetQueryFilter((LambdaExpression)filter!);
        entityData.AddIndex(entityData.FindProperty(nameof(ISoftDelete.DeleteLevel))!);
    }

    private static LambdaExpression GetCascadeSoftDeleteFilter<TEntity>()
        where TEntity : class, ISoftDelete
    {
        Expression<Func<TEntity, bool>> filter = x => x.DeleteLevel == 0;
        return filter;
    }
}
