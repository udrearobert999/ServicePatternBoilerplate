using AutoMapper;
using ServicePattern.Application.Dtos;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Application.Profiles;

internal sealed class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>();
    }
}