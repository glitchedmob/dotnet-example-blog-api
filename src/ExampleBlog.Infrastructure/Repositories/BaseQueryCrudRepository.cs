using ExampleBlog.Core.Domain.Common;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Extensions;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Infrastructure.Repositories;

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

        query = query.Skip(criteria.Offset).Take(criteria.Limit);

        return ApplyPagination(query, criteria);
    }

    public async Task<int> CountForCriteria(TQueryCriteriaType criteria)
    {
        return await QueryFromCriteriaWithoutPagination(criteria).CountAsync();
    }

    public IOrderedQueryable<TEntityType> OrderedQueryFromCriteria(TQueryCriteriaType criteria)
    {
        var query = QueryFromCriteria(criteria);

        return ApplySortCriteria(query, criteria);
    }

    protected IQueryable<TEntityType> QueryFromCriteriaWithoutPagination(TQueryCriteriaType criteria)
    {
        var query = DbSet.AsQueryable();

        if (criteria.IncludeDeleted)
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

    protected IOrderedQueryable<TEntityType> ApplySortCriteria(IQueryable<TEntityType> query,
        TQueryCriteriaType criteria)
    {
        return query.ApplyDynamicSorting(criteria.SortCriteria);
    }

    protected virtual IQueryable<TEntityType> ApplySearchCriteria(IQueryable<TEntityType> query, string searchText)
    {
        return query;
    }

    protected virtual IQueryable<TEntityType> ApplyCriteria(IQueryable<TEntityType> query, TQueryCriteriaType criteria)
    {
        return query;
    }
}
