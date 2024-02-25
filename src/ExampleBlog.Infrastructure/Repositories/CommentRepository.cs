﻿using ExampleBlog.Core.Domain;
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

    public IQueryable<Comment> QueryFromCriteria(CommentsQueryCriteria criteria)
    {
        var orderedQuery  = OrderedQueryFromDefaultCriteria<Comment, CommentsQueryCriteria, CommentSortableField>(criteria);

        var query = orderedQuery
            .WhereIf(criteria.Ids.Any(), e => criteria.Ids.Contains(e.Id))
            .WhereIf(criteria.PostIds.Any(), e => criteria.PostIds.Contains(e.PostId))
            .WhereIf(criteria.AuthorIds.Any(), e => criteria.AuthorIds.Contains(e.AuthorId));

        return query;
    }

    protected override IQueryable<Comment> ApplySearchCriteria(IQueryable<Comment> query, string searchText)
    {
        return query.Where(c => c.Content.Contains(searchText));
    }
}
