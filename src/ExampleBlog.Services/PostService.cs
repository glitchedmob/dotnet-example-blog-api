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

    public async Task<IEnumerable<Post>> GetMany(PostsQueryCriteria criteria)
    {
        var postsQuery = _context.Posts.AsQueryable();

        if (criteria.IncludeDeleted)
        {
            postsQuery = _softDeleteService.GetSoftDeletedEntries<Post>();
        }

        return await postsQuery.ToListAsync();
    }

    public async Task<int> GetCount(PostsQueryCriteria criteria)
    {
        return await _context.Posts.CountAsync();
    }

    public async Task<PaginatedResult<Post>> GetManyAndCount(PostsQueryCriteria criteria)
    {
        var items = await GetMany(criteria);
        var count = await GetCount(criteria);

        return new PaginatedResult<Post>
        {
            Items = items,
            Count = count,
            Limit = 0,
            Offset = 0,
        };
    }

    public async Task<Post?> GetById(int postId)
    {
        return await _context.Posts.FirstAsync(p => p.Id == postId);
    }

    public async Task<Post?> GetBySlug(string slug)
    {
        return await _context.Posts.FirstAsync(p => p.Slug == slug);
    }

    public async Task<Post> Create(CreatePost newPost)
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

    public async Task Delete(int postId)
    {
        var post = await _context.Posts.FirstAsync(p => p.Id == postId);

        await _softDeleteService.SetCascadeSoftDeleteAsync(post);
    }
}
