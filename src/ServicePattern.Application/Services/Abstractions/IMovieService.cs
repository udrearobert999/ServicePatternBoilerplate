using ServicePattern.Application.Dtos.Movies.Options;
using ServicePattern.Application.Dtos.Movies.Requests;
using ServicePattern.Application.Dtos.Movies.Responses;
using ServicePattern.Domain.Results;
using ServicePattern.Domain.Results.Generics;

namespace ServicePattern.Application.Services.Abstractions;

public interface IMovieService
{
    public Task<Result<CreateMovieResponseDto>> CreateAsync(CreateMovieRequestDto request,
        CancellationToken cancellationToken = default);

    public Task<Result<GetAllMoviesResponseDto>> GetAllPaginatedAsync(GetAllMoviesOptionsDto options,
        CancellationToken cancellationToken = default);

    public Task<Result<GetMovieResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<Result<UpdateMovieResponseDto>> UpdateAsync(Guid id, UpdateMovieRequestDto request,
        CancellationToken cancellationToken = default);

    public Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}