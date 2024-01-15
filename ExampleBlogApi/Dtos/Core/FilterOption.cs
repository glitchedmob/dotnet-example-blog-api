using Microsoft.AspNetCore.Mvc;

namespace ExampleBlogApi.Dtos.Core;

public class FilterOption<T>
{
    public FilterOption()
    {
        if (default(T) != null)
        {
            throw new InvalidOperationException("FilterOption generic must be nullable");
        }
    }

    [FromQuery(Name = "[eq]")]
    public T? EqualTo { get; set; }
    [FromQuery(Name = "[in]")]
    public T[]? In { get; set; }
    [FromQuery(Name = "[gt]")]
    public T? GreaterThan { get; set; }
    [FromQuery(Name = "[lt]")]
    public T? LessThan { get; set; }
    [FromQuery(Name = "[gte]")]
    public T? GreaterThanOrEqualTo { get; set; }
    [FromQuery(Name = "[lte]")]
    public T? LessThanOrEqualTo { get; set; }
}
