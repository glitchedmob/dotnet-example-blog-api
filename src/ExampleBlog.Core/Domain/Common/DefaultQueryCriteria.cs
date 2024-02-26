namespace ExampleBlog.Core.Domain.Common;

public class DefaultQueryCriteria
{
    public FieldFilterCriteria<DateTime?>? CreatedAt { get; set; }
    public FieldFilterCriteria<DateTime?>? UpdatedAt { get; set; }
    public string? SearchText { get; set; }
    public bool IncludeDeleted { get; set; } = false;
    public int Limit { get; set; }
    public int Offset { get; set; }
}

public abstract class DefaultQueryCriteria<TSortableFieldType> : DefaultQueryCriteria, ISortQueryCriteria<TSortableFieldType> where TSortableFieldType : Enum
{
    public abstract SortCriteria<TSortableFieldType> SortCriteria { get; set; }
}

public interface ISortQueryCriteria<TSortableFieldType> where TSortableFieldType : Enum
{
    SortCriteria<TSortableFieldType> SortCriteria { get; set; }
}
