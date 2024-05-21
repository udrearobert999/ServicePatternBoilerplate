﻿using FluentValidation;
using ServicePattern.Application.Dtos;
using ServicePattern.Application.Specifications;
using ServicePattern.Domain.Abstractions;

namespace ServicePattern.Application.Validation.Validators;

public class UpdateMovieRequestDtoValidator : AbstractValidator<UpdateMovieRequestDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMovieRequestDtoValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required!");

        RuleFor(x => x.Title)
            .MustAsync(TitleNotExists)
            .WithMessage("Title already exists!");
    }

    private async Task<bool> TitleNotExists(string title, CancellationToken cancellationToken)
    {
        var titleExistsSpec = new MovieTitleExistsSpec(title);
        var titleExists = await _unitOfWork.Movies.ExistsBySpecAsync(titleExistsSpec, cancellationToken);

        return !titleExists;
    }
}