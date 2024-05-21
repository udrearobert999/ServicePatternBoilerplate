namespace ServicePattern.Application.Dtos
{
    public class GetAllMoviesRequestDto : PaginatedRequestDto
    {
        public required string? Title { get; init; }
    }
}