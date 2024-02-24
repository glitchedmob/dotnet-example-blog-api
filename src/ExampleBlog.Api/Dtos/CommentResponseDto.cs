namespace ExampleBlog.Api.Dtos;

public class CommentResponseDto
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public required PostResponseDto Post { get; set; }
    public required AuthorResponseDto Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public class AuthorResponseDto
    {
        public required int Id { get; set; }
        public string? Email { get; set; }
    }

    public class PostResponseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
    }
}
