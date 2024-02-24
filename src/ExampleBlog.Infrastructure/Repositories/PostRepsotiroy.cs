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
        var query = QueryFromDefaultCriteria<Post>(criteria);

        if (criteria.Ids.Any())
        {
            query = query.Where(p => criteria.Ids.Contains(p.Id));
        }

        if (criteria.Slugs.Any())
        {
            query = query.Where(p => criteria.Slugs.Contains(p.Slug));
        }

        if (criteria.AuthorIds.Any())
        {
            query = query.Where(p => criteria.AuthorIds.Contains(p.AuthorId));
        }

        return query;
    }

    protected override IQueryable<Post> ApplySearchCriteria(IQueryable<Post> query, string searchText)
    {
        return query.Where(p => p.Title.Contains(searchText) || p.Content.Contains(searchText));
    }
}
