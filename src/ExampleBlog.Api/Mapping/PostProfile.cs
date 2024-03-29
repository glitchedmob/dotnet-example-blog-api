﻿using AutoMapper;
using ExampleBlog.Api.Dtos;
using ExampleBlog.Core.Domain;
using ExampleBlog.Core.Entities;

namespace ExampleBlog.Api.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostRequestDto, CreatePost>();
        this.CreateGetQueryDtoToCriteriaMapping<GetPostsRequestDto, PostsQueryCriteria, PostSortableField>();
        CreateMap<Post, PostResponseDto>();
        CreateMap<User, PostResponseDto.AuthorResponseDto>();
    }
}
