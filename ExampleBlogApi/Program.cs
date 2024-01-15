using System.Reflection;
using ExampleBlogApi.Database;
using ExampleBlogApi.Entities;
using ExampleBlogApi.Infrastructure.SoftDelete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoftDeleteServices.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.RegisterSoftDelServicesAndYourConfigurations(
    Assembly.GetAssembly(typeof(ConfigCascadeDelete)));

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
