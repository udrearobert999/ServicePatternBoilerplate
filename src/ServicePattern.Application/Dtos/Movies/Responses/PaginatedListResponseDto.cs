namespace ServicePattern.Application.Dtos.Movies.Responses;

public class PaginatedListResponseDto<TResponse>
{
    public required IEnumerable<TResponse> Items { get; init; } = Enumerable.Empty<TResponse>();
    public required int Total { get; set; }
}