using ExampleBlog.Core.Entities.Behaviors;
using Microsoft.AspNetCore.Identity;

namespace ExampleBlog.Core.Entities;

public class User : IdentityUser<int>, ISoftDelete
{
    public ICollection<Post> Posts { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;

    public byte DeleteLevel { get; set; }

    public DateTime? DeletedAt { get; set; }
}
