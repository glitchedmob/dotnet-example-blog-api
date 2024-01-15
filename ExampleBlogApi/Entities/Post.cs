using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExampleBlogApi.Database;
using ExampleBlogApi.Infrastructure.SoftDelete;
using ExampleBlogApi.Infrastructure.TimeStamped;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleBlogApi.Entities;

[Index(nameof(Slug), IsUnique = true)]
public class Post : ITimeStamped, ISoftDelete
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

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public byte SoftDeleteLevel { get; set; }

    public ICollection<Comment> Comments { get; set; } = default!;

    public class Configuration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
        }
    }
}
