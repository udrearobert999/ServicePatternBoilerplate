namespace ServicePattern.Application.Dtos;

public class PaginatedListResponseDto<TResponse>
{
    public required IEnumerable<TResponse> Items { get; init; } = Enumerable.Empty<TResponse>();
    public required int Total { get; set; }
}