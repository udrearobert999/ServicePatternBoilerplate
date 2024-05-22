using ServicePattern.Application.Dtos.Movies.Options;
using ServicePattern.Domain.Abstractions;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Application.Specifications;

internal sealed class CountMoviesByGetAllRequestSpec : Specification<Movie, Guid>
{
    public CountMoviesByGetAllRequestSpec(GetAllMoviesOptionsDto options) :
        base(m => options.Title == null || m.Title.Contains(options.Title))
    {
        DisableTracking();
    }
}