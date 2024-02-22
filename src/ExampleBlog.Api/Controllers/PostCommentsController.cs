using AutoMapper;
using ExampleBlog.Api.Dtos;
using ExampleBlog.Api.Routing;
using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleBlog.Api.Controllers;

[Route(RouteTemplates.PostComments)]
[ApiController]
public class PostCommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public PostCommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<CommentResponseDto>> CreateCommentForPost(int postId,
        [FromBody] CreateCommentRequestDto request)
    {
        var newComment = _mapper.Map<CreateComment>(request);

        var comment = await _commentService.CreateCommentForPost(postId, newComment);


        return CreatedAtAction(nameof(CommentsController.GetCommentById), "Comments", new { id = comment.Id },
            _mapper.Map<CommentResponseDto>(comment));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentResponseDto>>> GetComentsForPost(int postId)
    {
        var comments = await _commentService.GetCommentsForPost(postId);

        return Ok(_mapper.Map<IEnumerable<CommentResponseDto>>(comments));
    }
}
