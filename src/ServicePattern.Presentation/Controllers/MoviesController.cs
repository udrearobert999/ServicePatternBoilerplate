using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using ServicePattern.Application.Dtos;
using ServicePattern.Application.Services.Abstractions;
using ServicePattern.Presentation.Constants;

namespace ServicePattern.Presentation.Controllers;

public class MoviesController : BaseController
{
    private readonly IMovieService _movieService;
    private readonly IOutputCacheStore _outputCacheStore;

    public MoviesController(IMovieService movieService, IOutputCacheStore outputCacheStore)
    {
        _movieService = movieService;
        _outputCacheStore = outputCacheStore;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateMovieResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _movieService.CreateAsync(request, cancellationToken);

        if (result.IsFailure)
            return HandleFailure(result);

        await _outputCacheStore.EvictByTagAsync(CacheConstants.Keys.Movies, cancellationToken);

        var movie = result.Value;

        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    [HttpGet]
    [OutputCache(PolicyName = CacheConstants.Policies.Movies)]
    [ProducesResponseType(typeof(GetAllMoviesResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllMoviesRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _movieService.GetAllPaginatedAsync(request, cancellationToken);

        if (result.IsFailure)
            return HandleFailure(result);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _movieService.GetByIdAsync(id, cancellationToken);

        if (result.IsFailure)
            return HandleFailure(result);

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateMovieResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateMovieRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _movieService.UpdateAsync(id, request, cancellationToken);

        if (result.IsFailure)
            return HandleFailure(result);

        await _outputCacheStore.EvictByTagAsync(CacheConstants.Keys.Movies, cancellationToken);

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _movieService.DeleteAsync(id, cancellationToken);

        if (result.IsFailure)
            return HandleFailure(result);

        await _outputCacheStore.EvictByTagAsync(CacheConstants.Keys.Movies, cancellationToken);

        return NoContent();
    }
}