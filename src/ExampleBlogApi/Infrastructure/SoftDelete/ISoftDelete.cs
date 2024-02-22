namespace ExampleBlogApi.Infrastructure.SoftDelete;

public interface ISoftDelete
{
    byte DeleteLevel { get; set; }

    DateTime? DeletedAt { get; set; }
}
