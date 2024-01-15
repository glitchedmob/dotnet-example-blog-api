using Microsoft.AspNetCore.Mvc;

namespace ExampleBlogApi.Dtos.Core;

public class BaseGetQueryOptionsDto
{
    [FromQuery(Name = "searchText")]
    public string? SearchText { get; set; }
    [FromQuery(Name = "includeDeleted")]
    public bool IncludeDeleted { get; set; } = false;
    /// <summary>
    /// Sort options in the format of 'field:order', where 'order' can be 'asc' or 'desc'.
    /// Multiple sort options can be provided.
    /// </summary>
    [FromQuery(Name = "sort[]")]
    public List<SortOption> SortOptions { get; set; } = new();
}
