namespace ExampleBlog.Api.Infrastructure.TimeStamped;

public interface ITimeStamped
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
