using ServicePattern.Application.Dtos.Result.Constants;

namespace ServicePattern.Application.Dtos.Result.Errors;

internal record NotFoundError : Error
{
    public NotFoundError(string message) : base(ErrorCodes.NotFound, message)
    {
    }
}