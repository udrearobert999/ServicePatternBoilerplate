﻿namespace ServicePattern.Application.Dtos;

public class PagedResponseDto<TResponse>
{
    public required IEnumerable<TResponse> Items { get; init; } = Enumerable.Empty<TResponse>();
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public required int Total { get; set; }
    public bool HasNextPage => Total > (Page * PageSize);
}