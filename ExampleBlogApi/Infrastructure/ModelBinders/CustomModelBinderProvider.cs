using ExampleBlogApi.Dtos.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExampleBlogApi.Infrastructure.ModelBinders;

public class CustomModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(SortOption))
        {
            return new SortOptionModelBinder();
        }

        return null;
    }
}
