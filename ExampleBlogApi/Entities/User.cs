using Microsoft.AspNetCore.Identity;

namespace ExampleBlogApi.Entities;

public class User : IdentityUser<int>
{
    public ICollection<Post> Posts { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;
}
