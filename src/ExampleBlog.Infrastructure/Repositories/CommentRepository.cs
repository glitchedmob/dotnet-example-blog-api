using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Extensions;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Infrastructure.Repositories;

internal class CommentRepository : BaseCrudRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService) : base(
        context, softDeleteService)
    {
    }

    public IOrderedQueryable<Comment> QueryFromCriteria(CommentsQueryCriteria criteria)
    {
        var query  = QueryFromDefaultCriteria<Comment>(criteria);

        query = query
            .WhereIf(criteria.Ids.Any(), e => criteria.Ids.Contains(e.Id))
            .WhereIf(criteria.PostIds.Any(), e => criteria.PostIds.Contains(e.PostId))
            .WhereIf(criteria.AuthorIds.Any(), e => criteria.AuthorIds.Contains(e.AuthorId));

        return ApplySortCriteria<CommentsQueryCriteria, CommentSortableField>(query, criteria);
    }

    protected override IQueryable<Comment> ApplySearchCriteria(IQueryable<Comment> query, string searchText)
    {
        return query.Where(c => c.Content.Contains(searchText));
    }
}
