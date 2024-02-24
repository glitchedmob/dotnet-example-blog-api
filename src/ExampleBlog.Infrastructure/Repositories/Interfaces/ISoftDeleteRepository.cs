using ExampleBlog.Core.Entities.Behaviors;

namespace ExampleBlog.Infrastructure.Repositories.Interfaces;

public interface ISoftDeleteRepository<TEntityType> where TEntityType : class, ISoftDelete
{
    Task SoftDelete(TEntityType entity, CancellationToken ct = default);
}
