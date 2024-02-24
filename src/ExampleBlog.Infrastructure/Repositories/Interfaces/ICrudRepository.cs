using ExampleBlog.Core.Entities.Behaviors;

namespace ExampleBlog.Infrastructure.Repositories.Interfaces;

public interface ICrudRepository<TEntityType> where TEntityType : class, ISoftDelete
{
    IQueryable<TEntityType> NewQuery();
    void Add(TEntityType entity, CancellationToken ct = default);
    Task<int> SaveChanges(CancellationToken ct = default);
    void HardDelete(TEntityType entity);
    void Update(TEntityType entity);
    Task SoftDelete(TEntityType entity, CancellationToken ct = default);
}
