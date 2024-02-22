using System.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExampleBlog.Api.Infrastructure.ModelBinders;

public class NestedValueWithoutSuffixModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var queryStringKey = bindingContext.ModelName.Split(".")[0];

        if (!bindingContext.ActionContext.HttpContext.Request.Query.TryGetValue(queryStringKey, out var values))
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var optionType = bindingContext.ModelType;
        var isList = optionType.IsGenericType && optionType.GetGenericTypeDefinition() == typeof(List<>);
        var valueType = isList ? optionType.GetGenericArguments()[0] : optionType;
        var nullableUnderlyingType = Nullable.GetUnderlyingType(valueType);
        var isNullableType = nullableUnderlyingType != null;
        var targetType = isNullableType ? nullableUnderlyingType! : valueType!;

        if (isList)
        {
            var listType = typeof(List<>).MakeGenericType(valueType);
            var list = (IList)Activator.CreateInstance(listType)!;

            foreach (var valueString in values)
            {
                if (!string.IsNullOrEmpty(valueString))
                {
                    var value = Convert.ChangeType(valueString, targetType);
                    list.Add(value);
                }
            }

            bindingContext.Result = ModelBindingResult.Success(list);
        }
        else
        {
            var valueString = values.FirstOrDefault();
            if (!string.IsNullOrEmpty(valueString))
            {
                try
                {
                    var value = Convert.ChangeType(valueString, targetType);
                    bindingContext.Result = ModelBindingResult.Success(value);
                }
                catch
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                }
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }

        return Task.CompletedTask;
    }
}
