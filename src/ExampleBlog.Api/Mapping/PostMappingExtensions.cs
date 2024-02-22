using ExampleBlog.Api.Dtos;
using ExampleBlog.Api.Entities;

namespace ExampleBlog.Api.Mapping;

public static class PostMappingExtensions
{

    public static PostResponseDto ToDto(this Post post)
    {
        return new PostResponseDto
        {
            Id = post.Id,
            Slug = post.Slug,
            Title = post.Title,
            Content = post.Content,
            AuthorId = post.AuthorId,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
        };
    }
}
