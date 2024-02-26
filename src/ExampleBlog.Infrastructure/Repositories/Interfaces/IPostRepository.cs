using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;

namespace ExampleBlog.Infrastructure.Repositories.Interfaces;

public interface IPostRepository : ICrudRepository<Post>,
    IQueryCriteriaRepository<Post, PostsQueryCriteria, PostSortableField>,
    ISoftDeleteRepository<Post>;
