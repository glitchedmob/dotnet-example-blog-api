using System.Linq.Expressions;
using System.Reflection;
using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Infrastructure.Extensions;

internal static class QueryableExtensons
{
    public static IQueryable<TEntityType> ApplyFieldFilterCriteria<TEntityType, TDataType>(
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

    public static IOrderedQueryable<TEntityType> OrderBy<TEntityType>(
        this IQueryable<TEntityType> query,
        string property)
    {
        return ApplyOrder(query, property, "OrderBy");
    }

    public static IOrderedQueryable<TEntityType> OrderByDescending<TEntityType>(
        this IQueryable<TEntityType> query,
        string property)
    {
        return ApplyOrder(query, property, "OrderByDescending");
    }

    public static IOrderedQueryable<TEntityType> ThenBy<TEntityType>(
        this IOrderedQueryable<TEntityType> query,
        string property)
    {
        return ApplyOrder(query, property, "ThenBy");
    }

    public static IOrderedQueryable<TEntityType> ThenByDescending<TEntityType>(
        this IOrderedQueryable<TEntityType> query,
        string property)
    {
        return ApplyOrder(query, property, "ThenByDescending");
    }

    public static IOrderedQueryable<TEntityType> ApplyDynamicSorting<TEntityType, TSortableFieldType>(
        this IQueryable<TEntityType> source,
        IEnumerable<(TSortableFieldType, SortOrder)> sortCriteria)
        where TSortableFieldType : Enum
    {
        var allCriteria = (sortCriteria ?? []).ToList();

        if (!allCriteria.Any())
        {
            throw new ArgumentException(
                $"{nameof(sortCriteria)} was null or empty. {nameof(sortCriteria)} must have at least one item");
        }


        var properties = typeof(TEntityType).GetProperties();

        var (firstField, firstOrder) = allCriteria.First();
        var firstProperty = GetPropertyNameFromEnum(properties, firstField);

        var orderedQuery = firstOrder == SortOrder.Ascending
            ? source.OrderBy(firstProperty)
            : source.OrderByDescending(firstProperty);

        var otherCriteria = allCriteria.Skip(1).ToList();

        foreach (var (field, order) in otherCriteria)
        {
            var property = GetPropertyNameFromEnum(properties, field);

            orderedQuery = order == SortOrder.Ascending
                ? orderedQuery.ThenBy(property)
                : orderedQuery.ThenByDescending(property);
        }

        return orderedQuery;
    }


    private static IOrderedQueryable<TEntityType> ApplyOrder<TEntityType>(
        IQueryable<TEntityType> source,
        string property,
        string methodName)
    {
        var props = property.Split('.');
        var type = typeof(TEntityType);
        var arg = Expression.Parameter(type, "e");
        Expression expr = arg;
        foreach (var prop in props)
        {
            // use reflection (not ComponentModel) to mirror LINQ
            var pi = type.GetProperty(prop) ?? throw new InvalidOperationException();
            expr = Expression.Property(expr, pi);
            type = pi.PropertyType;
        }

        var delegateType = typeof(Func<,>).MakeGenericType(typeof(TEntityType), type);
        var lambda = Expression.Lambda(delegateType, expr, arg);

        var result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                          && method.IsGenericMethodDefinition
                          && method.GetGenericArguments().Length == 2
                          && method.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(TEntityType), type)
            .Invoke(null, [source, lambda]) ?? throw new InvalidOperationException();

        return (IOrderedQueryable<TEntityType>)result;
    }

    private static string GetPropertyNameFromEnum<TSortableFieldType>(PropertyInfo[] properties,
        TSortableFieldType field) where TSortableFieldType : Enum
    {
        foreach (var property in properties)
        {
            if (property.Name.Equals(field.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return property.Name;
            }
        }

        throw new ArgumentException($"No property found for enum value '{field}'");
    }
}
