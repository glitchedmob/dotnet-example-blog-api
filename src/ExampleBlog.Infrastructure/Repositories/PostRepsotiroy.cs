using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Infrastructure.Repositories;

internal class PostRepsotiroy : BaseCrudRepository<Post>, IPostRepository
{
    public PostRepsotiroy(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService) : base(
        context, softDeleteService)
    {
    }

    public IQueryable<Post> QueryFromCriteria(PostsQueryCriteria criteria)
    {
        throw new NotImplementedException();
    }

    protected override IQueryable<Post> ApplySearchCriteria(IQueryable<Post> query, string searchText)
    {
        throw new NotImplementedException();
    }
}
