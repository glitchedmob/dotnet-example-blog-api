namespace ExampleBlogApi.Dtos.Core;

public class SortOption
{
    public required string Field { get; set; }
    public SortOrder Order { get; set; }
}
