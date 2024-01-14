using ExampleBlogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlogApi.Database;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    protected AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Post> Posts { get; set; }
}
