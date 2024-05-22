using FluentValidation;
using ServicePattern.Application.Dtos.Movies.Requests;
using ServicePattern.Application.Specifications;
using ServicePattern.Domain.Abstractions;

namespace ServicePattern.Application.Validation.Movies.Requests;

public class CreateMovieRequestDtoValidator : AbstractValidator<CreateMovieRequestDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMovieRequestDtoValidator(IUnitOfWork unitOfWork)
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