using ExampleBlog.Api.Dtos;
using ExampleBlog.Api.Dtos.Common;
using ExampleBlog.Core.Domain.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExampleBlog.Api.Infrastructure.ModelBinders;

public class SortOptionModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;
        var parts = value?.Split(':') ?? [];
        if (parts.Length == 2)
        {
            var sortOption = new SortOption
            {
                Field = parts[0],
                Order = parts[1].ToLower() == "desc" ? SortOrder.Descending : SortOrder.Ascending
            };

            bindingContext.Result = ModelBindingResult.Success(sortOption);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(modelName, "Invalid sort option format.");
        }

        return Task.CompletedTask;
    }
}
