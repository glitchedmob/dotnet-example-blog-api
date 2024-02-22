using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Core.Domain;

public class PostsQueryCriteria : DefaultQueryCriteria
{
    public List<int> Ids { get; set; } = [];
    public List<string> Slugs { get; set; } = [];
    public List<int> AuthorIds { get; set; } = [];
}
