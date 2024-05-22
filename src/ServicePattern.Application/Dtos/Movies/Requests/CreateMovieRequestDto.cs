namespace ServicePattern.Application.Dtos.Movies.Requests;

public class CreateMovieRequestDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}