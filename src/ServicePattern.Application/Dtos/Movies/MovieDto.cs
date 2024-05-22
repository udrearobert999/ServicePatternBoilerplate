using ServicePattern.Application.Dtos.Genres;

namespace ServicePattern.Application.Dtos.Movies;

public class MovieDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public List<GenreDto> Genres { get; init; } = new List<GenreDto>();
}