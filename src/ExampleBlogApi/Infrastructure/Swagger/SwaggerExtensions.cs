using ExampleBlogApi.Dtos.Core;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ExampleBlogApi.Infrastructure.Swagger;

public static class SwaggerExtensions
{
    public static SwaggerGenOptions ApplySwaggerSchemaCustomizations(this SwaggerGenOptions options)
    {
        options.CustomSchemaIds(type =>
        {
            return GetSchemaIdForType(type);
        });
        options.MapType<SortOption>(() => new OpenApiSchema { Type = "string", Default = new OpenApiString("fieldName:asc") });
        options.OperationFilter<NestedValueWithoutSuffixOperationFilter>();
        return options;
    }

    private static string GetSchemaIdForType(Type t)
    {
        if (t.IsGenericType)
        {
            var genericTypeDefinition = t.GetGenericTypeDefinition().Name;
            genericTypeDefinition = genericTypeDefinition.Remove(genericTypeDefinition.IndexOf('`'));
            var genericArguments = string.Join("_", t.GetGenericArguments().Select(GetSchemaIdForType));
            return $"{genericTypeDefinition}Of{genericArguments}";
        }

        return t.IsNested && t.DeclaringType is not null ? $"{GetSchemaIdForType(t.DeclaringType)}_{t.Name}" : t.Name;
    }
}
