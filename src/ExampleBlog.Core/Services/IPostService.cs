using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;

namespace ExampleBlog.Core.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> GetMany(PostsQueryCriteria criteria);
    Task<int> GetCount(PostsQueryCriteria criteria);
    Task<PaginatedResult<Post>> GetManyAndCount(PostsQueryCriteria criteria);
    Task<Post> GetById(int postId);
    Task<Post> GetBySlug(string slug);
    Task<Post> Create(CreatePost newPost);
    Task Delete(int postId);
}
