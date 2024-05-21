using FluentValidation;
using ServicePattern.Application.Dtos;

namespace ServicePattern.Application.Validation.Validators;

public class PaginatedRequestDtoValidator : AbstractValidator<PaginatedRequestDto>
{
    public PaginatedRequestDtoValidator()
    {
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(25)
            .WithMessage("Page size cannot be greater than 25!");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page size cannot be less than 1!");
    }
}