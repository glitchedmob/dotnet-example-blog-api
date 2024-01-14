using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    [ForeignKey(nameof(Author))]
    public required int AuthorId { get; set; }

    public User Author { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;
}
