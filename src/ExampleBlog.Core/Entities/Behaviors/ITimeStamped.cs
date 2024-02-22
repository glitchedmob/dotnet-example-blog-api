namespace ExampleBlog.Core.Entities.Behaviors;

public interface ITimeStamped
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
