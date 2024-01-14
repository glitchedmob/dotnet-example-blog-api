using ExampleBlogApi.Entities.Core;
using Microsoft.AspNetCore.Identity;

namespace ExampleBlogApi.Entities;

public class User : IdentityUser<int>, ISoftDelete
{
    public ICollection<Post> Posts { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;

    public DateTime? DeletedAt { get; set; }
}
