using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Core.Domain;

public class CommentsQueryCriteria : DefaultQueryCriteria<CommentSortableField>
{
    public List<int> Ids { get; set; } = [];
    public List<int> AuthorIds { get; set; } = [];
    public List<int> PostIds { get; set; } = [];
    public override SortCriteria<CommentSortableField> SortCriteria { get; set; } = new()
    {
        { CommentSortableField.Id, SortOrder.Ascending }
    };
}
