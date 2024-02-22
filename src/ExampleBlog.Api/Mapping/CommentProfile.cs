using AutoMapper;
using ExampleBlog.Api.Dtos;
using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;

namespace ExampleBlog.Api.Mapping;

public class CommentProfile : Profile
{
    protected CommentProfile()
    {
        CreateMap<CreateCommentRequestDto, CreateComment>();
        CreateMap<GetCommentsRequestDto, CommentsQueryCriteria>();
        CreateMap<Comment, CommentResponseDto>();
    }
}
