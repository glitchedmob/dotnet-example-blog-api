using ExampleBlog.Core.Entities;
using ExampleBlog.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleBlog.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        return services;
    }

    public static IdentityBuilder AddIdentityServices(this IdentityBuilder builder)
    {
        return builder.AddIdentityInfrastructure();
    }
}
