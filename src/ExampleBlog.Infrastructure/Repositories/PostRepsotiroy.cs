﻿using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Extensions;
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

        query.WhereIf(criteria.Ids.Any(), e => criteria.Ids.Contains(e.Id));
        query.WhereIf(criteria.Slugs.Any(), e => criteria.Slugs.Contains(e.Slug));
        query.WhereIf(criteria.AuthorIds.Any(), e => criteria.AuthorIds.Contains(e.AuthorId));

        return query;
    }

    protected override IQueryable<Post> ApplySearchCriteria(IQueryable<Post> query, string searchText)
    {
        return query.Where(p => p.Title.Contains(searchText) || p.Content.Contains(searchText));
    }
}
