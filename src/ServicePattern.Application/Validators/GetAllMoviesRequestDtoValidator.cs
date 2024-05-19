using FluentValidation;
using ServicePattern.Application.Dtos;

namespace ServicePattern.Application.Validators;

public sealed class GetAllMoviesRequestDtoValidator : AbstractValidator<GetAllMoviesRequestDto>
{
    public GetAllMoviesRequestDtoValidator()
    {
        Include(new PagedRequestDtoValidator());
    }
}