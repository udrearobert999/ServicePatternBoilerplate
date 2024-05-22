using ServicePattern.Application.Dtos.Movies;

namespace ServicePattern.Application.Dtos.Movies.Responses;

public class GetAllMoviesResponseDto : PaginatedListResponseDto<MovieDto>
{
}