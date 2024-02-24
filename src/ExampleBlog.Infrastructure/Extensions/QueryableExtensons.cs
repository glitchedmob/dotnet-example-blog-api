using System.Linq.Expressions;
using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Infrastructure.Extensions;

internal static class QueryableExtensons
{
    public static IQueryable<TEntityType> ApplyFieldCriteria<TEntityType, TDataType>(
        this IQueryable<TEntityType> query,
        Expression<Func<TEntityType, TDataType>> propertySelector,
        FieldFilterCriteria<TDataType?>? fieldCriteria)
    {
        if (fieldCriteria is null)
        {
            return query;
        }

        if (fieldCriteria.Values.Any())
        {
            if (fieldCriteria.Values.Count == 1)
            {
                var value = fieldCriteria.Values.First();
                var constant = Expression.Constant(value, typeof(TDataType));
                var equalExpression = Expression.Equal(propertySelector.Body, constant);
                var lambda = Expression.Lambda<Func<TEntityType, bool>>(equalExpression, propertySelector.Parameters);
                query = query.Where(lambda);
            }
            else
            {
                var containsMethod = typeof(Enumerable).GetMethods()
                    .First(method => method.Name == "Contains" && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TDataType));
                var valuesExpression = Expression.Constant(fieldCriteria.Values, typeof(IEnumerable<TDataType>));
                var callExpression = Expression.Call(containsMethod, valuesExpression, propertySelector.Body);
                var lambda = Expression.Lambda<Func<TEntityType, bool>>(callExpression, propertySelector.Parameters);
                query = query.Where(lambda);
            }
        }

        query = ApplyComparisonCriteria(query, propertySelector, fieldCriteria.GreaterThan, Expression.GreaterThan);
        query = ApplyComparisonCriteria(query, propertySelector, fieldCriteria.GreaterThanOrEqualTo,
            Expression.GreaterThanOrEqual);
        query = ApplyComparisonCriteria(query, propertySelector, fieldCriteria.LessThan, Expression.LessThan);
        query = ApplyComparisonCriteria(query, propertySelector, fieldCriteria.LessThanOrEqualTo,
            Expression.LessThanOrEqual);

        return query;
    }

    private static IQueryable<TEntityType> ApplyComparisonCriteria<TEntityType, TDataType>(
        IQueryable<TEntityType> query,
        Expression<Func<TEntityType, TDataType>> propertySelector,
        TDataType? value,
        Func<Expression, Expression, BinaryExpression> comparisonFunc)
    {
        if (value is null)
        {
            return query;
        }

        var constant = Expression.Constant(value, typeof(TDataType?));
        var comparisonExpression = comparisonFunc(propertySelector.Body, constant);
        var lambda = Expression.Lambda<Func<TEntityType, bool>>(comparisonExpression, propertySelector.Parameters);
        return query.Where(lambda);
    }

    public static IQueryable<TEntityType> WhereIf<TEntityType>(this IQueryable<TEntityType> query, bool condition,
        Expression<Func<TEntityType, bool>> predicate)
    {
        return !condition ? query : query.Where(predicate);
    }
}
