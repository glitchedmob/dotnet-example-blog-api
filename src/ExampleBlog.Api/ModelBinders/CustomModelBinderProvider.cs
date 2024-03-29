using ExampleBlog.Api.Dtos.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ExampleBlog.Api.ModelBinders;

public class CustomModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(SortOption))
        {
            var binderType = typeof(SortOptionModelBinder);
            return new BinderTypeModelBinder(binderType);
        }

        return null;
    }
}
