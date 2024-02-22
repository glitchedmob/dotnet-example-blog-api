namespace ExampleBlogApi.Dtos;

public class PostResponseDto
{
    public int Id { get; set; }

    public required string Slug { get; set; }

    public required string Title { get; set; }

    public required string Content { get; set; }

    public required int AuthorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
