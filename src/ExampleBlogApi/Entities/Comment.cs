using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExampleBlogApi.Database;
using ExampleBlogApi.Infrastructure.SoftDelete;
using ExampleBlogApi.Infrastructure.TimeStamped;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleBlogApi.Entities;

public class Comment : ITimeStamped, ISoftDelete
{
    [Key]
    public int Id { get; set; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public required string Content { get; set; }

    [ForeignKey(nameof(Post))]
    public required int PostId { get; set; }

    public Post Post { get; set; } = default!;

    [ForeignKey(nameof(Author))]
    public required int AuthorId { get; set; }

    public User Author { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public byte DeleteLevel { get; set; }

    public DateTime? DeletedAt { get; set; }

    public class Configuration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
        }
    }
}
