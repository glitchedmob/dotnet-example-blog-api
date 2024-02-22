using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;

namespace ExampleBlog.Core.Services;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetComments(CommentsQueryCriteria criteria);
    Task<IEnumerable<Comment>> GetCommentsForPost(int postId);
    Task<Comment?> GetCommentById(int commentId);
    Task<Comment> CreateCommentForPost(int postId, CreateComment newComment);
    Task<Comment?> UpdateCommentById(int commentId, UpdateComment commentUpdate);
    Task DeleteCommentById(int commentId);
}
