using System.Runtime.Serialization;

namespace ExampleBlog.Core.Domain.Common;

public enum SortOrder
{
    [EnumMember(Value = "asc")]
    Ascending,
    [EnumMember(Value = "desc")]
    Descending,
}
