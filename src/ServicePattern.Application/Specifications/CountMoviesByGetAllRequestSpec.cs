using ServicePattern.Application.Dtos;
using ServicePattern.Domain.Abstractions;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Application.Specifications;

internal sealed class CountMoviesByGetAllRequestSpec : Specification<Movie, Guid>
{
    public CountMoviesByGetAllRequestSpec(GetAllMoviesRequestDto request) :
        base(m => request.Title == null || m.Title.Contains(request.Title))
    {
    }
}