using ServicePattern.Application.Dtos;
using ServicePattern.Application.Dtos.Result;
using ServicePattern.Application.Dtos.Result.Generics;

namespace ServicePattern.Application.Services.Abstractions;

public interface IMovieService
{
    public Task<Result<CreateMovieResponseDto>> CreateAsync(CreateMovieRequestDto request,
        CancellationToken cancellationToken = default);

    public Task<Result<GetAllMoviesResponseDto>> GetAllPagedAsync(GetAllMoviesRequestDto request,
        CancellationToken cancellationToken = default);

    public Task<Result<GetMovieResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<Result<UpdateMovieResponseDto>> UpdateAsync(Guid id, UpdateMovieRequestDto request,
        CancellationToken cancellationToken = default);

    public Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}