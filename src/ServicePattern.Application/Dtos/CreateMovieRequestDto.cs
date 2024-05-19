namespace ServicePattern.Application.Dtos;

public class CreateMovieRequestDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}