using ExampleBlogApi.Database;
using ExampleBlogApi.Dtos;
using ExampleBlogApi.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlogApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CommentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentResponseDto>>> GetComments(
        [FromQuery] bool includeDeleted = false)
    {
        var commentsQuery = _context.Comments.AsQueryable();

        if (includeDeleted)
        {
            commentsQuery = commentsQuery.IgnoreQueryFilters();
        }

        var comments = await commentsQuery.ToListAsync();

        return Ok(comments.Select(c => c.ToDto()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentResponseDto>> GetCommentById(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

        return Ok(comment!.ToDto());
    }
}
