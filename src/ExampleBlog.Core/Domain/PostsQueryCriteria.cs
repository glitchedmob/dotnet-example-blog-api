using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Core.Domain;

public class PostsQueryCriteria : DefaultQueryCriteria<PostSortableField>
{
    public List<int> Ids { get; set; } = [];
    public List<string> Slugs { get; set; } = [];
    public List<int> AuthorIds { get; set; } = [];
    public override SortCriteria<PostSortableField> SortCriteria { get; set; } = new()
    {
        { PostSortableField.Id, SortOrder.Ascending }
    };
}
