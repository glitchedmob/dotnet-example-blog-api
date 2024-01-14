using ExampleBlogApi.Dtos;
using ExampleBlogApi.Entities;

namespace ExampleBlogApi.Mapping;

public static class CommentMappingExtensions
{
    public static CommentResponseDto ToDto(this Comment comment)
    {
        return new CommentResponseDto
        {
            Id = comment.Id,
            Content = comment.Content,
            PostId = comment.PostId,
            AuthorId = comment.AuthorId,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
        };
    }
}
