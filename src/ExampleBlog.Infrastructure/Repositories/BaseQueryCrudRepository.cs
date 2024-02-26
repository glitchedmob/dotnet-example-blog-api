using ExampleBlog.Core.Domain.Common;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Extensions;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Infrastructure.Repositories;

/// <summary>
/// An extension of <see cref="BaseCrudRepository{TEntityType}"/> that provides more robust querying capabilities,
/// Specifically it has methods for automatically retrieving data based on a <see cref="TQueryCriteriaType"/>.
/// Be sure to override <see cref="ApplyCriteria"/> and <see cref="ApplySearchCriteria"/> for full funtionality
/// </summary>
/// <typeparam name="TEntityType">The entity type to perform CRUD and querying operations on</typeparam>
/// <typeparam name="TQueryCriteriaType">
/// The type for the criteria to apply. Intended to be a child of <see cref="DefaultQueryCriteria{TSortableFieldType}"/>
/// </typeparam>
/// <typeparam name="TSortableFieldType">Represents the sortable properties of <see cref="TEntityType"/> as an enum</typeparam>
internal abstract class BaseQueryCrudRepository<TEntityType, TQueryCriteriaType, TSortableFieldType> :
    BaseCrudRepository<TEntityType>, IQueryCriteriaRepository<TEntityType, TQueryCriteriaType, TSortableFieldType>
    where TEntityType : class, ISoftDelete, ITimeStamped
    where TSortableFieldType : Enum
    where TQueryCriteriaType : DefaultQueryCriteria<TSortableFieldType>
{
    protected BaseQueryCrudRepository(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService) :
        base(context, softDeleteService)
    {
    }

    public IQueryable<TEntityType> QueryFromCriteria(TQueryCriteriaType criteria)
    {
        var query = QueryFromCriteriaWithoutPagination(criteria);

        return ApplyPagination(query, criteria);
    }

    public async Task<int> CountForCriteria(TQueryCriteriaType criteria)
    {
        return await QueryFromCriteriaWithoutPagination(criteria).CountAsync();
    }

    public IOrderedQueryable<TEntityType> OrderedQueryFromCriteria(TQueryCriteriaType criteria)
    {
        var query = QueryFromCriteriaWithoutPagination(criteria);

        var orderedQuery = ApplySortCriteria(query, criteria);

        return ApplyPagination(orderedQuery, criteria);
    }

    protected IQueryable<TEntityType> QueryFromCriteriaWithoutPagination(TQueryCriteriaType criteria)
    {
        var query = DbSet.AsQueryable();

        if (criteria.OnlyDeleted)
        {
            query = SoftDeleteService.GetSoftDeletedEntries<TEntityType>();
        }

        query = query.ApplyFieldFilterCriteria(e => e.CreatedAt, criteria.CreatedAt);
        query = query.ApplyFieldFilterCriteria(e => e.UpdatedAt, criteria.UpdatedAt);

        if (criteria.SearchText is not null)
        {
            query = ApplySearchCriteria(query, criteria.SearchText);
        }

        query = ApplyCriteria(query, criteria);

        return query;
    }

    protected IQueryable<TEntityType> ApplyPagination(IQueryable<TEntityType> query, TQueryCriteriaType criteria)
    {
        return query.Skip(criteria.Offset).Take(criteria.Limit);
    }

    protected IOrderedQueryable<TEntityType> ApplyPagination(IOrderedQueryable<TEntityType> query, TQueryCriteriaType criteria)
    {
        return (IOrderedQueryable<TEntityType>)query.Skip(criteria.Offset).Take(criteria.Limit);
    }

    protected IOrderedQueryable<TEntityType> ApplySortCriteria(IQueryable<TEntityType> query,
        TQueryCriteriaType criteria)
    {
        return query.ApplyDynamicSorting(criteria.SortCriteria);
    }

    /// <summary>
    /// Use a <c>.Where()</c> on <see cref="query"/> to apply a search filter
    /// </summary>
    /// <param name="query">Query to modify</param>
    /// <param name="searchText">Text to search for</param>
    /// <returns></returns>
    /// <example>
    /// <code>
    /// protected override IQueryable&lt;Post&gt; ApplySearchCriteria(IQueryable&lt;Post&gt; query, string searchText)
    /// {
    ///    searchText = searchText.ToLower();
    ///    return query.Where(p => p.Title.ToLower().Contains(searchText) || p.Content.ToLower().Contains(searchText));
    /// }
    /// </code>
    /// </example>
    protected virtual IQueryable<TEntityType> ApplySearchCriteria(IQueryable<TEntityType> query, string searchText)
    {
        return query;
    }

    /// <summary>
    /// Use LINQ to add filters to a query based on what values are provided in <see cref="criteria"/>
    /// </summary>
    /// <param name="query">Query to modify</param>
    /// <param name="criteria">Criteria to apply</param>
    /// <returns></returns>
    /// <example>
    /// <code>
    /// protected override IQueryable&lt;Post&gt; ApplyCriteria(IQueryable&lt;Post&gt; query, PostsQueryCriteria criteria)
    /// {
    ///     return query
    ///         .WhereIf(criteria.Ids.Any(), e => criteria.Ids.Contains(e.Id))
    ///         .WhereIf(criteria.Slugs.Any(), e => criteria.Slugs.Contains(e.Slug))
    ///         .WhereIf(criteria.AuthorIds.Any(), e => criteria.AuthorIds.Contains(e.AuthorId));
    /// }
    /// </code>
    /// </example>
    protected virtual IQueryable<TEntityType> ApplyCriteria(IQueryable<TEntityType> query, TQueryCriteriaType criteria)
    {
        return query;
    }
}
