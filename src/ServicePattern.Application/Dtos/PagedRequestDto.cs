namespace ServicePattern.Application.Dtos;

public class PagedRequestDto
{
    public required int Page { get; init; } = 1;
    public required int PageSize { get; init; } = 10;
}