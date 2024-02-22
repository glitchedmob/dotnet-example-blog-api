namespace ExampleBlog.Core.Entities.Behaviors;

public interface ISoftDelete
{
    byte DeleteLevel { get; set; }

    DateTime? DeletedAt { get; set; }
}
