using ServicePattern.Domain.Results.Constants;

namespace ServicePattern.Domain.Results.Errors;

internal record NotFoundError : Error
{
    public NotFoundError(string message) : base(ErrorCodes.NotFound, message)
    {
    }
}