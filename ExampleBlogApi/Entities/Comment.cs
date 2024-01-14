using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExampleBlogApi.Entities;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Content { get; set; }

    [ForeignKey(nameof(Post))]
    [Required]
    public int PostId { get; set; }

    public Post Post { get; set; }
}
