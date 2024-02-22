using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ExampleBlog.Api.Routing;

public class QueryStringConstraint : IActionConstraint
{
    private readonly string _parameter;

    public QueryStringConstraint(string parameter)
    {
        _parameter = parameter;
    }

    public int Order => 0;

    public bool Accept(ActionConstraintContext context)
    {
        return context.RouteContext.HttpContext.Request.Query.ContainsKey(_parameter);
    }
}
