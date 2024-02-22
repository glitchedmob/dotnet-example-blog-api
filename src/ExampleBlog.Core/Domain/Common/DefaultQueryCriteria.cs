namespace ExampleBlog.Core.Domain.Common;

public class DefaultQueryCriteria
{
    public FieldFilterCriteria<DateTime?>? CreatedAt { get; set; }
    public FieldFilterCriteria<DateTime?>? UpdatedAt { get; set; }
    public string? SearchText { get; set; }
    public bool IncludeDeleted { get; set; } = false;
    public List<SortCriteria>? SortOptions { get; set; }
}
