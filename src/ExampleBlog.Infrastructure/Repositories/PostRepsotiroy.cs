﻿using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Extensions;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Infrastructure.Repositories;

internal class PostRepsotiroy : BaseQueryCrudRepository<Post, PostsQueryCriteria, PostSortableField>, IPostRepository
{
    public PostRepsotiroy(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService) : base(
        context, softDeleteService)
    {
    }

    protected override IQueryable<Post> ApplyCriteria(IQueryable<Post> query, PostsQueryCriteria criteria)
    {
        return query
            .WhereIf(criteria.Ids.Any(), e => criteria.Ids.Contains(e.Id))
            .WhereIf(criteria.Slugs.Any(), e => criteria.Slugs.Contains(e.Slug))
            .WhereIf(criteria.AuthorIds.Any(), e => criteria.AuthorIds.Contains(e.AuthorId));
    }

    protected override IQueryable<Post> ApplySearchCriteria(IQueryable<Post> query, string searchText)
    {
        return query.Where(p => p.Title.Contains(searchText) || p.Content.Contains(searchText));
    }
}
