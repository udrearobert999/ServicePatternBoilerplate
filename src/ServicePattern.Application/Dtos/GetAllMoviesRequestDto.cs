namespace ServicePattern.Application.Dtos
{
    public class GetAllMoviesRequestDto : PaginatedRequestDto
    {
        public string? Title { get; init; }
    }
}