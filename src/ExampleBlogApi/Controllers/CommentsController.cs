using ExampleBlogApi.Database;
using ExampleBlogApi.Dtos;
using ExampleBlogApi.Infrastructure.SoftDelete;
using ExampleBlogApi.Mapping;
using ExampleBlogApi.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftDeleteServices.Concrete;

namespace ExampleBlogApi.Controllers;

[Route(RouteTemplates.Comments)]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CascadeSoftDelServiceAsync<ISoftDelete> _softDeleteService;

    public CommentsController(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService)
    {
        _context = context;
        _softDeleteService = softDeleteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentResponseDto>>> GetComments(
        [FromQuery] GetCommentsRequestDto request)
    {
        var commentsQuery = _context.Comments.AsQueryable();

        if (request.IncludeDeleted)
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

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CommentResponseDto>> UpdateCommentById(int id, [FromBody] UpdateCommentRequestDto request)
    {
        var comment = await _context.Comments.FirstAsync(c => c.Id == id);

        comment.Content = request.Content;

        await _context.SaveChangesAsync();

        return Ok(comment.ToDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCommentById(int id)
    {
        var comment = await _context.Comments.FirstAsync(c => c.Id == id);

        await _softDeleteService.SetCascadeSoftDeleteAsync(comment);

        return NoContent();
    }
}
