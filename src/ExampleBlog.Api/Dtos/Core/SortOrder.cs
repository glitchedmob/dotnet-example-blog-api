using System.Runtime.Serialization;

namespace ExampleBlog.Api.Dtos.Core;

public enum SortOrder
{
    [EnumMember(Value = "asc")]
    Ascending,
    [EnumMember(Value = "desc")]
    Descending,
}
