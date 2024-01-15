using System.Runtime.Serialization;

namespace ExampleBlogApi.Dtos.Core;

public enum SortOrder
{
    [EnumMember(Value = "asc")]
    Ascending,
    [EnumMember(Value = "desc")]
    Descending,
}
