using ExampleBlogApi.Dtos;
using ExampleBlogApi.Dtos.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExampleBlogApi.Infrastructure.ModelBinders;

public class SortOptionModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
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
