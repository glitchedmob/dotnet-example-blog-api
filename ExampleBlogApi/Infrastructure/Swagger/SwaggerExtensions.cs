using ExampleBlogApi.Dtos.Core;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ExampleBlogApi.Infrastructure.Swagger;

public static class SwaggerExtensions
{
    public static SwaggerGenOptions ApplySwaggerSchemaCustomizations(this SwaggerGenOptions options)
    {
        options.MapType<SortOption>(() => new OpenApiSchema { Type = "string", Default = new OpenApiString("fieldName:asc") });
        options.OperationFilter<NestedValueWithoutSuffixOperationFilter>();
        return options;
    }
}
