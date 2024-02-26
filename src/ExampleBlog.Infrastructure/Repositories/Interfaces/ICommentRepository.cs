using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;

namespace ExampleBlog.Infrastructure.Repositories.Interfaces;

public interface ICommentRepository : ICrudRepository<Comment>,
    ISortQueryCriteriaRepository<Comment, CommentsQueryCriteria, CommentSortableField>,
    ISoftDeleteRepository<Comment>;
