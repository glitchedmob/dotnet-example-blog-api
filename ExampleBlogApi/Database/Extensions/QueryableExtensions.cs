namespace ExampleBlogApi.Database.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> IncludeSoftDeleted<T>(this IQueryable<T> query, AppDbContext context)
    {
        context.IncludeSoftDeletedEntities = true;
        return query;
    }
}
