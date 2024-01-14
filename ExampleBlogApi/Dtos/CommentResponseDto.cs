namespace ExampleBlogApi.Dtos;

public class CommentResponseDto
{
    public int Id { get; set; }

    public required string Content { get; set; }

    public required int PostId { get; set; }

    public required int AuthorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
