﻿using AutoMapper;
using ServicePattern.Application.Dtos;
using ServicePattern.Application.Dtos.Result;
using ServicePattern.Application.Services.Abstractions;
using ServicePattern.Application.Specifications;
using ServicePattern.Application.Validators;
using ServicePattern.Domain.Abstractions;
using ServicePattern.Domain.Entities;
using ServicePattern.Application.Dtos.Result.Generics;

namespace ServicePattern.Application.Services;

internal sealed class MovieService : IMovieService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidationOrchestrator _validationOrchestrator;

    public MovieService(IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidationOrchestrator validationOrchestrator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationOrchestrator = validationOrchestrator;
    }

    public async Task<Result<CreateMovieResponseDto>> CreateAsync(CreateMovieRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validationOrchestrator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsFailure)
        {
            return Result<CreateMovieResponseDto>.FromError(validationResult.Error);
        }

        var movie = _mapper.Map<Movie>(request);

        var createdMovie = await _unitOfWork.Movies.CreateAsync(movie, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var responseDto = _mapper.Map<CreateMovieResponseDto>(createdMovie);

        return Result<CreateMovieResponseDto>.Success(responseDto);
    }

    public async Task<Result<GetAllMoviesResponseDto>> GetAllPagedAsync(GetAllMoviesRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validationOrchestrator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsFailure)
        {
            return Result<GetAllMoviesResponseDto>.FromError(validationResult.Error);
        }

        var getMoviesWithGenresSpec = new GetAllMoviesWithGenresPagedSpec(request);
        var movies = await _unitOfWork.Movies.GetBySpecAsync(getMoviesWithGenresSpec, cancellationToken);

        var countMoviesSpec = new CountMoviesSpec(request);
        var moviesCount = await _unitOfWork.Movies.CountBySpecAsync(countMoviesSpec, cancellationToken);

        var responseDto = _mapper.Map<GetAllMoviesResponseDto>(movies);
        _mapper.Map(request, responseDto);
        responseDto.Total = moviesCount;

        return Result<GetAllMoviesResponseDto>.Success(responseDto);
    }

    public async Task<Result<GetMovieResponseDto>> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id, track: false, cancellationToken: cancellationToken);

        var responseDto = _mapper.Map<GetMovieResponseDto>(movie);

        return Result<GetMovieResponseDto>.Success(responseDto);
    }

    public async Task<Result<UpdateMovieResponseDto>> UpdateAsync(Guid id, UpdateMovieRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validationOrchestrator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsFailure)
        {
            return Result<UpdateMovieResponseDto>.FromError(validationResult.Error);
        }

        var movie = await _unitOfWork.Movies.GetByIdAsync(id, track: true, cancellationToken: cancellationToken);
        if (movie == null)
        {
            return Result<UpdateMovieResponseDto>.NotFound();
        }

        _mapper.Map(request, movie);

        await _unitOfWork.Movies.UpdateAsync(movie, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var responseDto = _mapper.Map<UpdateMovieResponseDto>(movie);

        return Result<UpdateMovieResponseDto>.Success(responseDto);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id, track: true, cancellationToken: cancellationToken);
        if (movie == null)
        {
            return Result.NotFound();
        }

        await _unitOfWork.Movies.UpdateAsync(movie, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}