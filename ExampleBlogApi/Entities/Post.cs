using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExampleBlogApi.Entities;

[Index(nameof(Slug), IsUnique = true)]
public class Post
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Slug { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public ICollection<Comment> Comments { get; set; }
}
