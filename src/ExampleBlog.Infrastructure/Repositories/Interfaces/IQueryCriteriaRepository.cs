using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Infrastructure.Repositories.Interfaces;

public interface
    IQueryCriteriaRepository<TEntityType, TQueryCriteriaType, TSortableFieldsType>
    where TEntityType : class
    where TSortableFieldsType : Enum
    where TQueryCriteriaType : DefaultQueryCriteria<TSortableFieldsType>
{
    IQueryable<TEntityType> QueryFromCriteria(TQueryCriteriaType criteria);
    Task<int> CountForCriteria(TQueryCriteriaType criteria);
    IOrderedQueryable<TEntityType> OrderedQueryFromCriteria(TQueryCriteriaType criteria);
}
