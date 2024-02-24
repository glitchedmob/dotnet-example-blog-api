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

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<Comment>> GetMany(CommentsQueryCriteria criteria)
    {
        return await _commentRepository.QueryFromCriteria(criteria).ToListAsync();
    }

    public async Task<int> GetCount(CommentsQueryCriteria criteria)
    {
        return await _commentRepository.QueryFromCriteria(criteria).CountAsync();
    }

    public async Task<PaginatedResult<Comment>> GetManyAndCount(CommentsQueryCriteria criteria)
    {
        var items = await GetMany(criteria);
        var count = await GetCount(criteria);

        return new PaginatedResult<Comment>
        {
            Items = items,
            Count = count,
            Limit = 0,
            Offset = 0,
        };
    }

    public async Task<IEnumerable<Comment>> GetManyForPost(int postId)
    {
        return await _commentRepository.NewQuery()
            .Where(c => c.PostId == postId)
            .ToListAsync();
    }

    public async Task<Comment?> GetByid(int commentId)
    {
        return await _commentRepository.NewQuery()
            .FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task<Comment> Create(int postId, CreateComment newComment)
    {
        var post = await _postRepository.NewQuery().FirstOrDefaultAsync(p => p.Id == postId);
        // var user = await _context.Users.FirstOrDefaultAsync();

        var comment = new Comment
        {
            Content = newComment.Content,
            PostId = post!.Id,
            AuthorId = 1,
        };

        _commentRepository.Add(comment);
        await _commentRepository.SaveChanges();

        return comment;
    }

    public async Task<Comment?> Update(int commentId, UpdateComment commentUpdate)
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
