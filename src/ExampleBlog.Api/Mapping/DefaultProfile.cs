using AutoMapper;
using ExampleBlog.Api.Dtos.Common;
using ExampleBlog.Core.Domain.Common;

namespace ExampleBlog.Api.Mapping;

public class DefaultProfile : Profile
{
    public DefaultProfile()
    {
        CreateMap(typeof(FilterOption<>), typeof(FieldFilterCriteria<>));
        CreateMap<DefaultGetQueryOptions, DefaultQueryCriteria>();
    }
}
