﻿using Asp.Versioning;
using ExampleBlogApi.Database;
using ExampleBlogApi.Dtos;
using ExampleBlogApi.Entities;
using ExampleBlogApi.Infrastructure.SoftDelete;
using ExampleBlogApi.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftDeleteServices.Concrete;

namespace ExampleBlogApi.Controllers;

[Route(RouteTemplates.Posts)]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CascadeSoftDelServiceAsync<ISoftDelete> _softDeleteService;

    public PostsController(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService)
    {
        _context = context;
        _softDeleteService = softDeleteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetPosts([FromQuery] bool includeDeleted = false)
    {
        var postsQuery = _context.Posts.AsQueryable();

        if (includeDeleted)
        {
            postsQuery = _softDeleteService.GetSoftDeletedEntries<Post>();
        }
        var posts = await postsQuery.ToListAsync();

        return Ok(posts.Select(p => p.ToDto()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        var post = await _context.Posts.FirstAsync(p => p.Id == id);

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<PostResponseDto>> CreatePost([FromBody] CreatePostRequestDto request)
    {
        var user = await _context.Users.FirstAsync();

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

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        var post = await _context.Posts.FirstAsync(p => p.Id == id);

        await _softDeleteService.SetCascadeSoftDeleteAsync(post);

        return NoContent();
    }
}