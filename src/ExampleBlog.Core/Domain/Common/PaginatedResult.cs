namespace ExampleBlog.Core.Domain.Common;

public class PaginatedResult<TItemType>
{
    public IEnumerable<TItemType> Items { get; set; } = [];
    public int Count { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int PageSize => Limit;
    public int CurrentPage => Limit <= 0 ? 1 : Offset / Limit + 1;
}
