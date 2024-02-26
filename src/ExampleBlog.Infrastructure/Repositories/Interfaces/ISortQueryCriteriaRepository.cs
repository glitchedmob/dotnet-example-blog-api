using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Infrastructure.Repositories.Interfaces;

public interface ISortQueryCriteriaRepository<TEntityType, TQueryCriteriaType, TSortableFieldsType>
    where TEntityType : class
    where TSortableFieldsType : Enum
    where TQueryCriteriaType : ISortQueryCriteria<TSortableFieldsType>
{
    IOrderedQueryable<TEntityType> QueryFromCriteria(TQueryCriteriaType criteria);
}
