using ExampleBlogApi.Dtos.Core;
using Microsoft.AspNetCore.Mvc;

namespace ExampleBlogApi.Dtos;

public class GetCommentsRequestDto : BaseGetQueryOptionsDto
{
    [FromQuery(Name = "id[]")]
    public List<int> Ids { get; set; }
    [FromQuery(Name = "authorId[]")]
    public List<int> AuthorIds { get; set; }
    [FromQuery(Name = "postId[]")]
    public List<int> PostIds { get; set; }
}
