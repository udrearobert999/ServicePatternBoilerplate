using ServicePattern.Application.Results.Abstractions;
using ServicePattern.Application.Results.Constants;

namespace ServicePattern.Application.Results.Errors;

internal record CustomError : Error
{
    public CustomError(string message) : base(ErrorCodes.UnknownError, message)
    {
    }

    public CustomError(string code, string message) : base(code, message)
    {
    }

    public CustomError(IEnumerable<IError> innerErrors) : base(
        ErrorCodes.UnknownError,
        nameof(ErrorCodes.UnknownError),
        innerErrors)
    {
    }

    public CustomError(string code, string message, IEnumerable<IError> innerErrors) : base(
        code,
        message,
        innerErrors)
    {
    }
}