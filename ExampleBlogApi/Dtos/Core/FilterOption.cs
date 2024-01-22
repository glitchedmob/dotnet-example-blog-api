using ExampleBlogApi.Infrastructure.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExampleBlogApi.Dtos.Core;

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
    public List<TOptionType> Values { get; set; } = new();
    [FromQuery(Name = "gt")]
    public TOptionType? GreaterThan { get; set; }
    [FromQuery(Name = "lt")]
    public TOptionType? LessThan { get; set; }
    [FromQuery(Name = "gte")]
    public TOptionType? GreaterThanOrEqualTo { get; set; }
    [FromQuery(Name = "lte")]
    public TOptionType? LessThanOrEqualTo { get; set; }
}


public class GetProductsDto
{
    [FromQuery(Name = "createdAt")]
    public FilterOption<DateTime?> CreatedAt { get; set; }
    [FromQuery(Name = "price")]
    public FilterOption<int?> Price { get; set; }
}
