using ServicePattern.Application.Dtos.Movies.Options;
using ServicePattern.Domain.Abstractions;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Application.Specifications;

internal sealed class GetAllMoviesWithGenresPaginatedSpec : Specification<Movie, Guid>
{
    public GetAllMoviesWithGenresPaginatedSpec(GetAllMoviesOptionsDto options) :
        base(m => options.Title == null || m.Title.Contains(options.Title))
    {
        AddInclude(x => x.Genres);
        ApplyPaging(options.Page, options.PageSize);
        ApplyOrderBy(options.OrderBy, options.OrderDirection);
        DisableTracking();
    }
}