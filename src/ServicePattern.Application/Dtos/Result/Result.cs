using ServicePattern.Application.Dtos.Result.Abstractions;
using ServicePattern.Application.Dtos.Result.Constants;
using ServicePattern.Application.Dtos.Result.Errors.Factory;

namespace ServicePattern.Application.Dtos.Result;

public record Result
{
    public IError? Error { get; }
    public bool IsSuccess => !HasError();
    public bool IsFailure => HasError();

    protected Result()
    {
    }

    protected Result(IError error)
    {
        Error = error;
    }

    protected bool HasError()
    {
        return Error is not null;
    }

    public static Result Success() => new();
    public static Result NotFound() => new(ErrorFactory.NotFound());
    public static Result NotFound(string message) => new(ErrorFactory.NotFound(message));
    public static Result ValidationFailure() => new(ErrorFactory.ValidationFailure());
    public static Result ValidationFailure(string message) => new(ErrorFactory.ValidationFailure(message));
    public static Result FromError(IError error) => new(error);

    public bool IsValidationFailureResult()
    {
        return HasError() && Error!.Code == ErrorCodes.ValidationFailure;
    }

    public bool IsNotFoundResult()
    {
        return HasError() && Error!.Code == ErrorCodes.NotFound;
    }
}