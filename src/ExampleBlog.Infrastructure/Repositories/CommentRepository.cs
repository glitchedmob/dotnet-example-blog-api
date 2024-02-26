using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Extensions;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Infrastructure.Repositories;

internal class CommentRepository : BaseQueryCrudRepository<Comment, CommentsQueryCriteria, CommentSortableField>,
    ICommentRepository
{
    public CommentRepository(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService) : base(
        context, softDeleteService)
    {
    }

    protected override IQueryable<Comment> ApplyCriteria(IQueryable<Comment> query, CommentsQueryCriteria criteria)
    {
        return query
            .WhereIf(criteria.Ids.Any(), e => criteria.Ids.Contains(e.Id))
            .WhereIf(criteria.PostIds.Any(), e => criteria.PostIds.Contains(e.PostId))
            .WhereIf(criteria.AuthorIds.Any(), e => criteria.AuthorIds.Contains(e.AuthorId));
    }

    protected override IQueryable<Comment> ApplySearchCriteria(IQueryable<Comment> query, string searchText)
    {
        return query.Where(c => c.Content.Contains(searchText));
    }
}
