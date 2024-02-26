using AutoMapper;
using ExampleBlog.Api.Dtos;
using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;

namespace ExampleBlog.Api.Mapping;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CreateCommentRequestDto, CreateComment>();
        this.CreateGetQueryDtoToCriteriaMapping<GetCommentsRequestDto, CommentsQueryCriteria, CommentSortableField>();
        CreateMap<Comment, CommentListItemResponseDto>();
        CreateMap<Comment, CommentResponseDto>();
        CreateMap<User, CommentResponseDto.AuthorResponseDto>();
        CreateMap<Post, CommentResponseDto.PostResponseDto>();
    }
}
