using System.Reflection;
using ExampleBlog.Core.Entities;
using ExampleBlog.Infrastructure.Repositories;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using ExampleBlog.Infrastructure.SoftDelete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoftDeleteServices.Configuration;

namespace ExampleBlog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.RegisterSoftDelServicesAndYourConfigurations(
            Assembly.GetAssembly(typeof(ConfigCascadeDelete)));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPostRepository, PostRepsotiroy>();

        return services;
    }

    public static IdentityBuilder AddIdentityInfrastructure(this IdentityBuilder builder)
    {
        return builder.AddEntityFrameworkStores<AppDbContext>();
    }
}
