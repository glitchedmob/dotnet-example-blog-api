namespace ExampleBlog.Api.Dtos;

public class CreatePostRequestDto
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? Slug { get; set; }
}
