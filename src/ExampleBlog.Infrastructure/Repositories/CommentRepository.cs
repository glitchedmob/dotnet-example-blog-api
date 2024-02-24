using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Infrastructure.Repositories;

internal class CommentRepository : BaseCrudRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService) : base(context, softDeleteService)
    {
    }

    public IQueryable<Comment> QueryFromCriteria(CommentsQueryCriteria criteria)
    {
        var query = QueryFromDefaultCriteria<Comment>(criteria);

        if (criteria.Ids.Any())
        {
            query = query.Where(c => criteria.Ids.Contains(c.Id));
        }

        if (criteria.AuthorIds.Any())
        {
            query = query.Where(c => criteria.AuthorIds.Contains(c.AuthorId));
        }

        if (criteria.PostIds.Any())
        {
            query = query.Where(c => criteria.PostIds.Contains(c.PostId));
        }

        return query;
    }

    protected override IQueryable<Comment> ApplySearchCriteria(IQueryable<Comment> query, string searchText)
    {
        return query.Where(c => c.Content.Contains(searchText));
    }
}
