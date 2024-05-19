using FluentValidation;
using ServicePattern.Application.Dtos;

namespace ServicePattern.Application.Validators;

public class PagedRequestDtoValidator: AbstractValidator<PagedRequestDto>
{
    public PagedRequestDtoValidator()
    {
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(25)
            .WithMessage("Page size cannot be greater than 25!");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page size cannot be less than 1!");
    }
}