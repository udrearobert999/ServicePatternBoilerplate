namespace ServicePattern.Application.Dtos
{
    public class GetAllMoviesRequestDto : PagedRequestDto
    {
        public required string? Title { get; init; }
    }
}