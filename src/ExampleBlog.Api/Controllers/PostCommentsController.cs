using ExampleBlog.Api.Database;
using ExampleBlog.Api.Dtos;
using ExampleBlog.Api.Routing;
using ExampleBlog.Api.Mapping;
using ExampleBlog.Core.Entities;
using ExampleBlog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlog.Api.Controllers;

[Route(RouteTemplates.PostComments)]
[ApiController]
public class PostCommentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PostCommentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<CommentResponseDto>> CreateCommentForPost(int postId,
        [FromBody] CreateCommentRequestDto request)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        var user = await _context.Users.FirstOrDefaultAsync();

        var comment = new Comment
        {
            Content = request.Content,
            PostId = post!.Id,
            AuthorId = user!.Id,
        };

        _context.Add(comment);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CommentsController.GetCommentById), "Comments", new { id = comment.Id },
            comment.ToDto());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentResponseDto>>> GetComentsForPost(int postId)
    {
        var comments = await _context.Comments.Where(c => c.PostId == postId).ToListAsync();

        return Ok(comments.Select(c => c.ToDto()));
    }
}
