using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Api.Dtos.Common;

public class SortOption
{
    public required string Field { get; set; }
    public SortOrder Order { get; set; }
}
