using AutoMapper;
using ExampleBlog.Api.Dtos;
using ExampleBlog.Api.Routing;
using ExampleBlog.Api.Mapping;
using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleBlog.Api.Controllers;

[Route(RouteTemplates.Posts)]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;

    public PostsController(IPostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
    }

    [HttpGet(Name = nameof(GetPosts))]
    public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetPosts([FromQuery] GetPostsRequestDto request)
    {
        var criteria = _mapper.Map<PostsQueryCriteria>(request);

        var posts = await _postService.GetMany(criteria);

        return Ok(_mapper.Map<IEnumerable<PostResponseDto>>(posts));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostResponseDto>> GetPostById(int id)
    {
        var post = await _postService.GetById(id);

        return Ok(_mapper.Map<PostResponseDto>(post));
    }

    [HttpGet("slug/{slug}")]
    public async Task<ActionResult<PostResponseDto>> GetPostBySlug(string slug)
    {
        var post = await _postService.GetBySlug(slug);

        return Ok(_mapper.Map<PostResponseDto>(post));
    }

    [HttpPost]
    public async Task<ActionResult<PostResponseDto>> CreatePost([FromBody] CreatePostRequestDto request)
    {
        var newPost = _mapper.Map<CreatePost>(request);

        var post = await _postService.Create(newPost);

        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, _mapper.Map<PostResponseDto>(post));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        await _postService.Delete(id);

        return NoContent();
    }
}
