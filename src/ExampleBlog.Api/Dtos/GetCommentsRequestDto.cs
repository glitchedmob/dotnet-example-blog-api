using ExampleBlog.Api.Dtos.Core;
using Microsoft.AspNetCore.Mvc;

namespace ExampleBlog.Api.Dtos;

public class GetCommentsRequestDto : BaseGetQueryOptionsDto
{
    [FromQuery(Name = "id")]
    public List<int> Ids { get; set; } = new();
    [FromQuery(Name = "authorId")]
    public List<int> AuthorIds { get; set; } = new();
    [FromQuery(Name = "postId")]
    public List<int> PostIds { get; set; } = new();
}
