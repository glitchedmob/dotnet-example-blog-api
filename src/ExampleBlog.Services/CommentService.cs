using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Core.Services;
using ExampleBlog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Services;

internal class CommentService : ICommentService
{
    private readonly AppDbContext _context;
    private readonly CascadeSoftDelServiceAsync<ISoftDelete> _softDeleteService;

    public CommentService(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService)
    {
        _context = context;
        _softDeleteService = softDeleteService;
    }

    public async Task<IEnumerable<Comment>> GetMany(CommentsQueryCriteria criteria)
    {
        var commentsQuery = _context.Comments.AsQueryable();

        if (criteria.IncludeDeleted)
        {
            commentsQuery = commentsQuery.IgnoreQueryFilters();
        }

        return await commentsQuery.ToListAsync();
    }

    public async Task<int> GetCount(CommentsQueryCriteria criteria)
    {
        return await _context.Comments.CountAsync();
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
        return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
    }

    public async Task<Comment?> GetByid(int commentId)
    {
        return await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task<Comment> Create(int postId, CreateComment newComment)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        var user = await _context.Users.FirstOrDefaultAsync();

        var comment = new Comment
        {
            Content = newComment.Content,
            PostId = post!.Id,
            AuthorId = user!.Id,
        };

        _context.Add(comment);

        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment?> Update(int commentId, UpdateComment commentUpdate)
    {
        var comment = await _context.Comments.FirstAsync(c => c.Id == commentId);

        comment.Content = commentUpdate.Content;

        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task Delete(int commentId)
    {
        var comment = await _context.Comments.FirstAsync(c => c.Id == commentId);

        await _softDeleteService.SetCascadeSoftDeleteAsync(comment);
    }
}
