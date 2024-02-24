using AutoMapper;
using ExampleBlog.Api.Dtos;
using ExampleBlog.Api.Dtos.Common;
using ExampleBlog.Api.Routing;
using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleBlog.Api.Controllers;

[Route(RouteTemplates.Comments)]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponseDto<CommentResponseDto>>> GetComments(
        [FromQuery] GetCommentsRequestDto request)
    {
        var criteria = _mapper.Map<CommentsQueryCriteria>(request);

        var result = await _commentService.GetManyAndCount(criteria);

        return Ok(_mapper.Map<PaginatedResponseDto<CommentResponseDto>>(result));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentResponseDto>> GetCommentById(int id)
    {
        var comment = await _commentService.GetByid(id);

        return Ok(_mapper.Map<CommentResponseDto>(comment!));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CommentResponseDto>> UpdateCommentById(int id, [FromBody] UpdateCommentRequestDto request)
    {
        var commentUpdate = _mapper.Map<UpdateComment>(request);

        var comment = await _commentService.Update(id, commentUpdate);

        return Ok(_mapper.Map<CommentResponseDto>(comment!));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCommentById(int id)
    {
        await _commentService.Delete(id);

        return NoContent();
    }
}
