using AutoMapper;
using ExampleBlog.Api.Dtos.Common;
using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Api.Mapping;

public static class MappingExtensions
{
    public static IMappingExpression<TGetQueryType, TCriteriaType> CreateGetQueryDtoToCriteriaMapping<TGetQueryType,
        TCriteriaType, TSortableFieldType>(this Profile profile)
        where TGetQueryType : DefaultGetQueryOptions
        where TSortableFieldType : struct, Enum
        where TCriteriaType : DefaultQueryCriteria<TSortableFieldType>
    {
        return profile.CreateMap<TGetQueryType, TCriteriaType>()
            .AfterMap((src, dest, ctx) =>
            {
                dest.SortCriteria =
                    new SortOptionsToSortCriteriaConverter<TSortableFieldType>().Convert(src.SortOptions,
                        dest.SortCriteria, ctx);
            });
    }
}
