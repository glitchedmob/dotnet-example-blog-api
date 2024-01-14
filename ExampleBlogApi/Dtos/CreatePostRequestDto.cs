namespace ExampleBlogApi.Dtos;

public class CreatePostRequestDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Slug { get; set; }
}
