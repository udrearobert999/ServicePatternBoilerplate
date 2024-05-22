namespace ServicePattern.Application.Dtos.Shared;

public class BaseGetCollectionOptionsDto
{
    public int? Page { get; init; }
    public int? PageSize { get; init; }
    public string? OrderBy { get; init; }
    public string? OrderDirection { get; init; }
}