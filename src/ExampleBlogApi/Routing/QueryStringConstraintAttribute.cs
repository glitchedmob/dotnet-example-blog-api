using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ExampleBlogApi.Routing;

[AttributeUsage(AttributeTargets.Method)]
public class QueryStringConstraintAttribute : Attribute, IActionConstraintFactory
{
    private readonly string _parameter;
    private readonly bool _isReusable = true;

    public QueryStringConstraintAttribute(string parameter)
    {
        _parameter = parameter;
    }

    public IActionConstraint CreateInstance(IServiceProvider services)
    {
        return new QueryStringConstraint(_parameter);
    }

    public bool IsReusable => _isReusable;
}
