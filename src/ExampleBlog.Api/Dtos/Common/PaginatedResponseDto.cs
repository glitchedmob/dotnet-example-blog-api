namespace ExampleBlog.Api.Dtos.Common;

public class PaginatedResponseDto<TItemType>
{
    public IEnumerable<TItemType> Items { get; set; } = [];
    public int Count { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
}
