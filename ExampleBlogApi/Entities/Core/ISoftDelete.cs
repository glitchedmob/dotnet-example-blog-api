namespace ExampleBlogApi.Entities.Core;

public interface ISoftDelete
{
    public DateTime? DeletedAt { get; set; }
}
