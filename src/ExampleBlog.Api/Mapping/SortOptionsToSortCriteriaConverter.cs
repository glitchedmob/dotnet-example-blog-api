using AutoMapper;
using ExampleBlog.Api.Dtos.Common;
using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Api.Mapping;

public class SortOptionsToSortCriteriaConverter<TSortableFieldType> : ITypeConverter<List<SortOption>?, SortCriteria<TSortableFieldType>> where TSortableFieldType : struct, Enum
{
    public SortCriteria<TSortableFieldType> Convert(List<SortOption>? source, SortCriteria<TSortableFieldType> destination, ResolutionContext context)
    {
        var newDestination = new SortCriteria<TSortableFieldType>();
        foreach (var sortOption in source ?? [])
        {
            if (Enum.TryParse<TSortableFieldType>(sortOption.Field, ignoreCase: true, out var field))
            {
                newDestination.Add(field, sortOption.Order);
            }
        }

        return newDestination.Any() ? newDestination : destination;
    }
}
