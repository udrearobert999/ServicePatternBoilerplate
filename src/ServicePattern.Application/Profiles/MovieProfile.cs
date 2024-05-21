using AutoMapper;
using ServicePattern.Application.Dtos;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Application.Profiles;

internal class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>()
            .ForMember(dto => dto.Genres, opt => opt.MapFrom(src => src.Genres));

        CreateMap<Movie, GetMovieResponseDto>();

        CreateMap<CreateMovieRequestDto, Movie>();
        CreateMap<Movie, CreateMovieResponseDto>();
        
        CreateMap<UpdateMovieRequestDto, Movie>();
        CreateMap<Movie, UpdateMovieResponseDto>();

        CreateMap<IEnumerable<Movie>, GetAllMoviesResponseDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));
    }
}