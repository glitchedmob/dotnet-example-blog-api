using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Core.Domain;

public class CommentsQueryCriteria : DefaultQueryCriteria
{
    public List<int> Ids { get; set; } = [];
    public List<int> AuthorIds { get; set; } = [];
    public List<int> PostIds { get; set; } = [];
}
