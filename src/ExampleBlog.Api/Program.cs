using System.Reflection;
using Asp.Versioning;
using ExampleBlog.Api.Database;
using ExampleBlog.Api.Entities;
using ExampleBlog.Api.Infrastructure.ModelBinders;
using ExampleBlog.Api.Infrastructure.SoftDelete;
using ExampleBlog.Api.Infrastructure.Swagger;
using ExampleBlog.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using SoftDeleteServices.Configuration;

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


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.RegisterSoftDelServicesAndYourConfigurations(
    Assembly.GetAssembly(typeof(ConfigCascadeDelete)));

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

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
