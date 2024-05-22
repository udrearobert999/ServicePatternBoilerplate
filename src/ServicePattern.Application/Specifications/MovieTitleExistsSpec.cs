using ServicePattern.Domain.Abstractions;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Application.Specifications;

internal sealed class MovieTitleExistsSpec : Specification<Movie, Guid>
{
    public MovieTitleExistsSpec(string title) :
        base(x => x.Title == title)
    {
        DisableTracking();
    }
}