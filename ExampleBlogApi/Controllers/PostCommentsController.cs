using ExampleBlogApi.Database;
using ExampleBlogApi.Dtos;
using ExampleBlogApi.Entities;
using ExampleBlogApi.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlogApi.Controllers;

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
    public async Task<ActionResult<CommentResponseDto>> CreatePost(int postId,
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
}
