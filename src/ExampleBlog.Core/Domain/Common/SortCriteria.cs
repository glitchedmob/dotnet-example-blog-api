namespace ExampleBlog.Core.Domain.Common;

public class SortCriteria
{
    public required string Field { get; set; }
    public SortOrder Order { get; set; }
}
