using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlogApi.Entities;

[Index(nameof(Slug), IsUnique = true)]
public class Post
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public required string Slug { get; set; }

    [MaxLength(255)]
    public required string Title { get; set; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public required string Content { get; set; }

    public ICollection<Comment> Comments { get; set; } = default!;
}
