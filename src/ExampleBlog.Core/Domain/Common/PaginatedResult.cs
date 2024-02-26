namespace ExampleBlog.Core.Domain.Common;

/// <summary>
/// Represents the result of a call for data that requires pagination.
/// Supports both <see cref="Limit"/> and <see cref="Offset"/> as well as <see cref="PageSize"/> and <see cref="CurrentPage"/>
/// </summary>
/// <typeparam name="TItemType">The type of each item</typeparam>
public class PaginatedResult<TItemType>
{
    public IEnumerable<TItemType> Items { get; set; } = [];
    public int Count { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int PageSize => Limit;
    public int CurrentPage => Limit <= 0 ? 1 : Offset / Limit + 1;
}
