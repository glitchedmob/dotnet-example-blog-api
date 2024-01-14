using ExampleBlogApi.Database;
using ExampleBlogApi.Database.Extensions;
using ExampleBlogApi.Dtos;
using ExampleBlogApi.Entities;
using ExampleBlogApi.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlogApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PostsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetPosts([FromQuery] bool includeDeleted = false)
    {
        var postsQuery = _context.Posts.AsQueryable();

        if (includeDeleted)
        {
            postsQuery = postsQuery.IgnoreQueryFilters();
        }
        var posts = await postsQuery.ToListAsync();

        return Ok(posts.Select(p => p.ToDto()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<PostResponseDto>> CreatePost([FromBody] CreatePostRequestDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync();

        var post = new Post
        {
            Title = request.Title,
            Content = request.Content,
            Slug = request.Slug ?? request.Title.ToLower().Replace(" ", "-"),
            AuthorId = user!.Id,
        };

        _context.Add(post);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post.ToDto());
    }
}
