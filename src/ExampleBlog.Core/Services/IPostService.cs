using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;

namespace ExampleBlog.Core.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPosts(PostsQueryCriteria criteria);
    Task<Post?> GetPostById(int postId);
    Task<Post?> GetPostBySlug(string slug);
    Task<Post> CreatePost(CreatePost newPost);
    Task DeletePostById(int postId);
}
