namespace ExampleBlog.Core.Domain.Common;

public abstract class DefaultQueryCriteria<TSortableFieldType> where TSortableFieldType : Enum
{
    public FieldFilterCriteria<DateTime?>? CreatedAt { get; set; }
    public FieldFilterCriteria<DateTime?>? UpdatedAt { get; set; }
    public string? SearchText { get; set; }
    public bool IncludeDeleted { get; set; } = false;
    public int Limit { get; set; }
    public int Offset { get; set; }
    public abstract SortCriteria<TSortableFieldType> SortCriteria { get; set; }
}
