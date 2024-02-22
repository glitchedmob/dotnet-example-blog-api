using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Core.Services;
using ExampleBlog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Services;

public class PostService : IPostService
{
    private readonly AppDbContext _context;
    private readonly CascadeSoftDelServiceAsync<ISoftDelete> _softDeleteService;

    public PostService(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService)
    {
        _context = context;
        _softDeleteService = softDeleteService;
    }

    public async Task<IEnumerable<Post>> GetPosts(PostsQueryCriteria criteria)
    {
        var postsQuery = _context.Posts.AsQueryable();

        if (criteria.IncludeDeleted)
        {
            postsQuery = _softDeleteService.GetSoftDeletedEntries<Post>();
        }

        return await postsQuery.ToListAsync();
    }

    public async Task<Post?> GetPostById(int postId)
    {
        return await _context.Posts.FirstAsync(p => p.Id == postId);
    }

    public async Task<Post?> GetPostBySlug(string slug)
    {
        return await _context.Posts.FirstAsync(p => p.Slug == slug);
    }

    public async Task<Post> CreatePost(CreatePost newPost)
    {
        var user = await _context.Users.FirstAsync();

        var post = new Post
        {
            Title = newPost.Title,
            Content = newPost.Content,
            Slug = newPost.Slug ?? newPost.Title.ToLower().Replace(" ", "-"),
            AuthorId = user!.Id,
        };

        _context.Add(post);

        await _context.SaveChangesAsync();

        return post;
    }

    public async Task DeletePostById(int postId)
    {
        var post = await _context.Posts.FirstAsync(p => p.Id == postId);

        await _softDeleteService.SetCascadeSoftDeleteAsync(post);
    }
}
