using System.Reflection;
using Asp.Versioning;
using ExampleBlog.Api.Mapping;
using ExampleBlog.Api.ModelBinders;
using ExampleBlog.Api.Swagger;
using ExampleBlog.Core.Entities;
using ExampleBlog.Application;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options =>
    {
        options.ModelBinderProviders.Insert(0, new CustomModelBinderProvider());
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });
builder.Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
        options.AssumeDefaultVersionWhenUnspecified = true;
    })
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string? xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.ApplySwaggerSchemaCustomizations();
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityApiEndpoints<User>()
    .AddIdentityServices();

builder.Services.AddAutoMapper(typeof(DefaultProfile).Assembly);

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
