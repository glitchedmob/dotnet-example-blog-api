namespace ExampleBlogApi;

public static class RouteTemplates
{
    public const string Base = "api/v{version:apiVersion}";
    public const string Posts = $"{Base}/posts";
    public const string Comments = $"{Base}/comments";
    public const string PostComments = $"{Posts}/{{postId:int}}/comments";
}
