namespace ExampleBlog.Core.Domain.Common;

public class FieldFilterCriteria<TFieldType>
{
    public List<TFieldType> Values { get; set; } = [];
    public TFieldType? GreaterThan { get; set; }
    public TFieldType? LessThan { get; set; }
    public TFieldType? GreaterThanOrEqualTo { get; set; }
    public TFieldType? LessThanOrEqualTo { get; set; }
}
