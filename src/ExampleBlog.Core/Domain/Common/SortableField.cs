namespace ExampleBlog.Core.Domain.Common;

public class SortableField
{
    public static readonly SortableField Id = new(nameof(Id));
    public static readonly SortableField CreatedAt = new(nameof(CreatedAt));
    public static readonly SortableField UpdatedAt = new(nameof(UpdatedAt));

    private string FieldName { get; }

    protected SortableField(string fieldName)
    {
        FieldName = fieldName;
    }

    public override string ToString() => FieldName;

    public static SortableField Parse(string fieldName, IEnumerable<SortableField>? additionalFields = null)
    {
        var knownFields = new List<SortableField> { Id, CreatedAt, UpdatedAt };
        if (additionalFields is not null)
        {
            knownFields.AddRange(additionalFields);
        }

        var field = knownFields.FirstOrDefault(f => f.FieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        if (field is not null)
        {
            return field;
        }

        throw new ArgumentException($"Unknown field: {fieldName}", nameof(fieldName));
    }
}
