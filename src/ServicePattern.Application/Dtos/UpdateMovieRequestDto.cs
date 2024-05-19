namespace ServicePattern.Application.Dtos;

public class UpdateMovieRequestDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}