namespace ExampleBlog.Core.Domain.Common;

/// <summary>
/// A parent type for all QueryCriteria. Supports:
/// <list type="bullet">
///     <item>
///         <term>Filtering by timestamps</term>
///     </item>
///     <item>
///         <term>Searching</term>
///     </item>
///     <item>
///         <term>Getting only soft deleted entities</term>
///     </item>
///     <item>
///         <term>Pagination</term>
///     </item>
///     <item>
///         <term>Sorting</term>
///     </item>
/// </list>
/// Note: Make sure to override <see cref="SortCriteria"/> with the default set of properties to sort by
/// </summary>
/// <typeparam name="TSortableFieldType">An enum representing the supported properties to sort by</typeparam>
public abstract class DefaultQueryCriteria<TSortableFieldType> where TSortableFieldType : Enum
{
    public FieldFilterCriteria<DateTime?>? CreatedAt { get; set; }
    public FieldFilterCriteria<DateTime?>? UpdatedAt { get; set; }
    public string? SearchText { get; set; }
    public bool OnlyDeleted { get; set; } = false;
    public int Limit { get; set; }
    public int Offset { get; set; }
    public abstract SortCriteria<TSortableFieldType> SortCriteria { get; set; }
}
