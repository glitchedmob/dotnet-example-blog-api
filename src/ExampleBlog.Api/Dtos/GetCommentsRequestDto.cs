using ExampleBlog.Api.Dtos.Common;
using Microsoft.AspNetCore.Mvc;

namespace ExampleBlog.Api.Dtos;

public class GetCommentsRequestDto : DefaultGetQueryOptions
{
    [FromQuery(Name = "id")]
    public List<int> Ids { get; set; } = [];
    [FromQuery(Name = "authorId")]
    public List<int> AuthorIds { get; set; } = [];
    [FromQuery(Name = "postId")]
    public List<int> PostIds { get; set; } = [];
}
