using ServicePattern.Application.Dtos.Movies.Options;
using ServicePattern.Application.Validation.Shared;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Application.Validation.Movies.Options;

public class GetAllMoviesOptionsDtoValidator :
    BaseGetCollectionOptionsDtoValidator<GetAllMoviesOptionsDto, Movie>
{
    public GetAllMoviesOptionsDtoValidator()
    {
    }
}