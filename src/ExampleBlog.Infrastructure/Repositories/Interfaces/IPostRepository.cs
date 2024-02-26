using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Domain.Common;
using ExampleBlog.Core.Entities;

namespace ExampleBlog.Infrastructure.Repositories.Interfaces;

public interface IPostRepository : ICrudRepository<Post>,
    ISortQueryCriteriaRepository<Post, PostsQueryCriteria, PostSortableField>,
    ISoftDeleteRepository<Post>;
