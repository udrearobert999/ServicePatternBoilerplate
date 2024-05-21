using ServicePattern.Application.Results.Constants;

namespace ServicePattern.Application.Results.Errors;

internal record NotFoundError : Error
{
    public NotFoundError(string message) : base(ErrorCodes.NotFound, message)
    {
    }
}