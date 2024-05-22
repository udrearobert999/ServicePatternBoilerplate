namespace ServicePattern.Application.Dtos.Movies.Requests;

public class CreateMovieResponseDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}