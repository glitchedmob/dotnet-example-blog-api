using ExampleBlogApi.Dtos.Core;
using Microsoft.AspNetCore.Mvc;

namespace ExampleBlogApi.Dtos;

public class GetPostsRequestDto : BaseGetQueryOptionsDto
{
    [FromQuery(Name = "id[]")]
    public List<int>? Ids { get; set; }
    [FromQuery(Name = "slug[]")]
    public List<string>? Slugs { get; set; }
    [FromQuery(Name = "authorId[]")]
    public List<int>? AuthorIds { get; set; }
}

