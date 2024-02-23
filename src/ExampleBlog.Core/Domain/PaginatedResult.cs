namespace ExampleBlog.Core.Domain;

public class PaginatedResult<TItemType>
{
    public IEnumerable<TItemType> Items { get; set; } = [];
    public int Count { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
}
