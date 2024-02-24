using ExampleBlog.Api.Infrastructure.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace ExampleBlog.Api.Dtos.Common;

public class FilterOption<TOptionType>
{
    public FilterOption()
    {
        if (default(TOptionType) != null)
        {
            throw new InvalidOperationException($"{nameof(FilterOption<TOptionType>)} generic must be nullable");
        }
    }

    [ModelBinder(BinderType = typeof(NestedValueWithoutSuffixModelBinder))]
    public List<TOptionType> Values { get; set; } = [];
    [FromQuery(Name = "gt")]
    public TOptionType? GreaterThan { get; set; }
    [FromQuery(Name = "lt")]
    public TOptionType? LessThan { get; set; }
    [FromQuery(Name = "gte")]
    public TOptionType? GreaterThanOrEqualTo { get; set; }
    [FromQuery(Name = "lte")]
    public TOptionType? LessThanOrEqualTo { get; set; }
}

