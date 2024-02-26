using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Services;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlog.Services;

internal class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Comment>> GetMany(CommentsQueryCriteria criteria)
    {
        return await _commentRepository.OrderedQueryFromCriteria(criteria).ToListAsync();
    }

    public async Task<int> GetCount(CommentsQueryCriteria criteria)
    {
        return await _commentRepository.CountForCriteria(criteria);
    }

    public async Task<PaginatedResult<Comment>> GetManyAndCount(CommentsQueryCriteria criteria)
    {
        var items = await GetMany(criteria);
        var count = await GetCount(criteria);

        return new PaginatedResult<Comment>
        {
            Items = items,
            Count = count,
            Limit = criteria.Limit,
            Offset = criteria.Offset,
        };
    }

    public async Task<IEnumerable<Comment>> GetManyForPost(int postId)
    {
        return await _commentRepository.NewQuery()
            .Where(c => c.PostId == postId)
            .ToListAsync();
    }

    public async Task<Comment> GetByid(int commentId)
    {
        return await _commentRepository.NewQuery()
            .Include(c => c.Post)
            .Include(c => c.Author)
            .FirstAsync(c => c.Id == commentId);
    }

    public async Task<Comment> Create(int postId, CreateComment newComment)
    {
        var post = await _postRepository.NewQuery().FirstAsync(p => p.Id == postId);
        var user = await _userRepository.NewQuery().FirstAsync();

        var comment = new Comment
        {
            Content = newComment.Content,
            PostId = post.Id,
            AuthorId = user.Id,
        };

        _commentRepository.Add(comment);
        await _commentRepository.SaveChanges();

        return comment;
    }

    public async Task<Comment> Update(int commentId, UpdateComment commentUpdate)
    {
        var comment = await GetByid(commentId);

        comment.Content = commentUpdate.Content;

        _commentRepository.Update(comment);

        await _commentRepository.SaveChanges();

        return comment;
    }

    public async Task Delete(int commentId)
    {
        var comment = await GetByid(commentId);
        await _commentRepository.SoftDelete(comment);
    }
}
