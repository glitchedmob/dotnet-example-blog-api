using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Domain.Common;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Services;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlog.Services;

internal class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public PostService(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Post>> GetMany(PostsQueryCriteria criteria)
    {
        return await _postRepository.OrderedQueryFromCriteria(criteria)
            .Include(p => p.Author)
            .ToListAsync();
    }

    public async Task<int> GetCount(PostsQueryCriteria criteria)
    {
        return await _postRepository.CountForCriteria(criteria);
    }

    public async Task<PaginatedResult<Post>> GetManyAndCount(PostsQueryCriteria criteria)
    {
        var items = await GetMany(criteria);
        var count = await GetCount(criteria);

        return new PaginatedResult<Post>
        {
            Items = items,
            Count = count,
            Limit = criteria.Limit,
            Offset = criteria.Offset,
        };
    }

    public async Task<Post> GetById(int postId)
    {
        return await  _postRepository.NewQuery()
            .Include(p => p.Author)
            .FirstAsync(p => p.Id == postId);
    }

    public async Task<Post> GetBySlug(string slug)
    {
        return await  _postRepository.NewQuery()
            .Include(p => p.Author)
            .FirstAsync(p => p.Slug == slug);
    }

    public async Task<Post> Create(CreatePost newPost)
    {
        var user = await _userRepository.NewQuery().FirstAsync();

        var post = new Post
        {
            Title = newPost.Title,
            Content = newPost.Content,
            Slug = newPost.Slug ?? newPost.Title.ToLower().Replace(" ", "-"),
            AuthorId = user.Id,
        };

        _postRepository.Add(post);
        await _postRepository.SaveChanges();

        return post;
    }

    public async Task Delete(int postId)
    {
        var post = await GetById(postId);
        await _postRepository.SoftDelete(post);
    }
}
