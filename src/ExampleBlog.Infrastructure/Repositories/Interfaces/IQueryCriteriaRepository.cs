namespace ExampleBlog.Infrastructure.Repositories.Interfaces;

public interface IQueryCriteriaRepository<TEntityType, TQueryCriteriaType> where TEntityType : class
{
    IQueryable<TEntityType> QueryFromCriteria(TQueryCriteriaType criteria);
}
