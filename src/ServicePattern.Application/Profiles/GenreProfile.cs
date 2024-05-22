using AutoMapper;
using ServicePattern.Application.Dtos.Genres;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Application.Profiles;

internal class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>();
    }
}