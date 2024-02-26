using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Domain.Common;
using ExampleBlog.Core.Entities;

namespace ExampleBlog.Core.Services;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetMany(CommentsQueryCriteria criteria);
    Task<int> GetCount(CommentsQueryCriteria criteria);
    Task<PaginatedResult<Comment>> GetManyAndCount(CommentsQueryCriteria criteria);
    Task<IEnumerable<Comment>> GetManyForPost(int postId);
    Task<Comment> GetByid(int commentId);
    Task<Comment> Create(int postId, CreateComment newComment);
    Task<Comment> Update(int commentId, UpdateComment commentUpdate);
    Task Delete(int commentId);
}
