using ServicePattern.Domain.Results.Abstractions;
using ServicePattern.Domain.Results.Constants;

namespace ServicePattern.Domain.Results.Errors;

internal record ValidationFailureError : Error
{
    public ValidationFailureError(string message) : base(ErrorCodes.ValidationFailure, message)
    {
    }

    public ValidationFailureError(IEnumerable<IError> innerErrors) : base(
        ErrorCodes.ValidationFailure,
        nameof(ErrorCodes.ValidationFailure),
        innerErrors)
    {
    }

    public ValidationFailureError(string code, string message) : base(code, message)
    {
    }

    public ValidationFailureError(string code, string message, IEnumerable<IError> innerErrors) : base(
        code,
        message,
        innerErrors)
    {
    }
}