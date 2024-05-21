using ServicePattern.Application.Dtos;
using ServicePattern.Application.Results;
using ServicePattern.Application.Results.Generics;

namespace ServicePattern.Application.Services.Abstractions;

public interface IMovieService
{
    public Task<Result<CreateMovieResponseDto>> CreateAsync(CreateMovieRequestDto request,
        CancellationToken cancellationToken = default);

    public Task<Result<GetAllMoviesResponseDto>> GetAllPaginatedAsync(GetAllMoviesRequestDto request,
        CancellationToken cancellationToken = default);

    public Task<Result<GetMovieResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<Result<UpdateMovieResponseDto>> UpdateAsync(Guid id, UpdateMovieRequestDto request,
        CancellationToken cancellationToken = default);

    public Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}