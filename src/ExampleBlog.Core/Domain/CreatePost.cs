namespace ExampleBlog.Core.Domain;

public class CreatePost
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? Slug { get; set; }
}
