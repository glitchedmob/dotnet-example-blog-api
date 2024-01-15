namespace ExampleBlogApi.Infrastructure.SoftDelete;

public interface ISoftDelete
{
    byte SoftDeleteLevel { get; set; }
}
