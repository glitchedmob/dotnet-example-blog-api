using ExampleBlogApi.Dtos;
using ExampleBlogApi.Entities;

namespace ExampleBlogApi.Mapping;

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
