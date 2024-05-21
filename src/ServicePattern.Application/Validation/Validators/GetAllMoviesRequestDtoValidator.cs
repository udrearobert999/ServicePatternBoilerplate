using FluentValidation;
using ServicePattern.Application.Dtos;

namespace ServicePattern.Application.Validation.Validators;

public class GetAllMoviesRequestDtoValidator : AbstractValidator<GetAllMoviesRequestDto>
{
    public GetAllMoviesRequestDtoValidator()
    {
        Include(new PaginatedRequestDtoValidator());
    }
}