using ExampleBlog.Api.Dtos.Common;
using Microsoft.AspNetCore.Mvc;

namespace ExampleBlog.Api.Dtos;

public class GetPostsRequestDto : DefaultGetQueryOptions
{
    [FromQuery(Name = "id")]
    public List<int> Ids { get; set; } = [];
    [FromQuery(Name = "slug")]
    public List<string> Slugs { get; set; } = [];
    [FromQuery(Name = "authorId")]
    public List<int> AuthorIds { get; set; } = [];
}

