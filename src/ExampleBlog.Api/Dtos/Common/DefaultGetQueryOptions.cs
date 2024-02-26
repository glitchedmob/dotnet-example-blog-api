using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExampleBlog.Api.Dtos.Common;

public class DefaultGetQueryOptions
{
    [FromQuery(Name = "createdAt")]
    public FilterOption<DateTime?>? CreatedAt { get; set; }
    [FromQuery(Name = "updatedAt")]
    public FilterOption<DateTime?>? UpdatedAt { get; set; }
    [FromQuery(Name = "searchText")]
    public string? SearchText { get; set; }
    [FromQuery(Name = "onlyDeleted")]
    public bool OnlyDeleted { get; set; } = false;
    /// <summary>
    /// Sort options in the format of 'field:order', where 'order' can be 'asc' or 'desc'.
    /// Multiple sort options can be provided.
    /// </summary>
    [FromQuery(Name = "sort")]
    public List<SortOption>? SortOptions { get; set; }
    [FromQuery(Name = "pageSize")]
    [Range(1, 200)]
    public int PageSize { get; set; } = 10;
    [FromQuery(Name = "currentPage")]
    [Range(1, int.MaxValue)]
    public int CurrentPage { get; set; } = 1;
    [BindNever]
    public int Limit => PageSize;
    [BindNever]
    public int Offset => (CurrentPage - 1) * PageSize;
}
