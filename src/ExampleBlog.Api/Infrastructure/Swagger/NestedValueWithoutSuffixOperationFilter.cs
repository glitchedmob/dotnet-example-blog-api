using ExampleBlog.Api.Infrastructure.ModelBinders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ExampleBlog.Api.Infrastructure.Swagger;

public class NestedValueWithoutSuffixOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            return;

        foreach (var parameter in operation.Parameters.ToList())
        {
            var paramDescriptor = context.ApiDescription.ParameterDescriptions
                .FirstOrDefault(p => p.Name == parameter.Name);

            if (paramDescriptor is null || paramDescriptor.BindingInfo?.BinderType != typeof(NestedValueWithoutSuffixModelBinder))
            {
                continue;
            }

            var index = operation.Parameters.IndexOf(parameter);
            operation.Parameters[index] = new OpenApiParameter
            {
                Name = parameter.Name.Replace($".{paramDescriptor.ModelMetadata.Name}", ""),
                In = parameter.In,
                Description = parameter.Description,
                Required = parameter.Required,
                Schema = parameter.Schema
            };
        }
    }
}
