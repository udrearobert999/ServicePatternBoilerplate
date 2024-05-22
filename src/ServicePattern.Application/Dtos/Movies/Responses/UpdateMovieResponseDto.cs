namespace ServicePattern.Application.Dtos.Movies.Responses;

public class UpdateMovieResponseDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
}