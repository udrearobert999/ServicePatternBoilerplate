using ServicePattern.Application.Dtos;
using ServicePattern.Domain.Abstractions;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Application.Specifications;

internal sealed class GetAllMoviesWithGenresPagedSpec : Specification<Movie, Guid>
{
    public GetAllMoviesWithGenresPagedSpec(GetAllMoviesRequestDto request) :
        base(m => request.Title == null || m.Title.Contains(request.Title))
    {
        AddInclude(x => x.Genres);
        ApplyPaging(request.Page, request.PageSize);
    }
}