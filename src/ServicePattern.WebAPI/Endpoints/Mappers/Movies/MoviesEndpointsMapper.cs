using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using ServicePattern.Application.Dtos.Movies.Options;
using ServicePattern.Application.Dtos.Movies.Requests;
using ServicePattern.Application.Dtos.Movies.Responses;
using ServicePattern.Application.Services.Abstractions;
using ServicePattern.WebAPI.Caching.Constants;
using ServicePattern.WebAPI.Endpoints.Constants;
using ServicePattern.WebAPI.Endpoints.Mappers.Abstractions;

namespace ServicePattern.WebAPI.Endpoints.Mappers.Movies
{
    public class MoviesEndpointsMapper : IEndpointsMapper
    {
        public void MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(EndpointsConstants.Movies.Base);

            group.MapPost(EndpointsConstants.Movies.Create, CreateMovie)
                .Produces<CreateMovieResponseDto>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName(nameof(CreateMovie));

            group.MapGet(EndpointsConstants.Movies.GetAll, GetAllMovies)
                .Produces<GetAllMoviesResponseDto>()
                .CacheOutput(CacheConstants.Policies.Movies)
                .WithName(nameof(GetAllMovies));

            group.MapGet(EndpointsConstants.Movies.Get, GetMovieById)
                .Produces<GetMovieResponseDto>()
                .Produces(StatusCodes.Status404NotFound)
                .WithName(nameof(GetMovieById));

            group.MapPut(EndpointsConstants.Movies.Update, UpdateMovie)
                .Produces<UpdateMovieResponseDto>()
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName(nameof(UpdateMovie));

            group.MapDelete(EndpointsConstants.Movies.Delete, DeleteMovie)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName(nameof(DeleteMovie));
        }

        public async Task<IResult> CreateMovie([FromBody] CreateMovieRequestDto request,
            IMovieService movieService,
            IOutputCacheStore outputCacheStore,
            CancellationToken cancellationToken)
        {
            var result = await movieService.CreateAsync(request, cancellationToken);

            if (result.IsFailure)
                return EndpointUtils.HandleFailure(result);

            await outputCacheStore.EvictByTagAsync(CacheConstants.Keys.Movies, cancellationToken);

            var movie = result.Value;

            return Results.CreatedAtRoute(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        public async Task<IResult> GetAllMovies([AsParameters] GetAllMoviesOptionsDto options,
            IMovieService movieService,
            CancellationToken cancellationToken)
        {
            var result = await movieService.GetAllPaginatedAsync(options, cancellationToken);

            if (result.IsFailure)
                return EndpointUtils.HandleFailure(result);

            return Results.Ok(result.Value);
        }

        public async Task<IResult> GetMovieById([FromRoute] Guid id,
            IMovieService movieService,
            CancellationToken cancellationToken)
        {
            var result = await movieService.GetByIdAsync(id, cancellationToken);

            if (result.IsFailure)
                return EndpointUtils.HandleFailure(result);

            return Results.Ok(result.Value);
        }

        public async Task<IResult> UpdateMovie([FromRoute] Guid id,
            [FromBody] UpdateMovieRequestDto request,
            IMovieService movieService,
            IOutputCacheStore outputCacheStore,
            CancellationToken cancellationToken)
        {
            var result = await movieService.UpdateAsync(id, request, cancellationToken);

            if (result.IsFailure)
                return EndpointUtils.HandleFailure(result);

            await outputCacheStore.EvictByTagAsync(CacheConstants.Keys.Movies, cancellationToken);

            return Results.Ok(result.Value);
        }


        public async Task<IResult> DeleteMovie([FromRoute] Guid id,
            IMovieService movieService,
            IOutputCacheStore outputCacheStore,
            CancellationToken cancellationToken)
        {
            var result = await movieService.DeleteAsync(id, cancellationToken);

            if (result.IsFailure)
                return EndpointUtils.HandleFailure(result);

            await outputCacheStore.EvictByTagAsync(CacheConstants.Keys.Movies, cancellationToken);

            return Results.NoContent();
        }
    }
}