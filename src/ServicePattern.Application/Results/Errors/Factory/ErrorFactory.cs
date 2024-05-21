﻿using ServicePattern.Application.Results.Abstractions;
using ServicePattern.Application.Results.Abstractions.Errors.Factory;
using ServicePattern.Application.Results.Constants;

namespace ServicePattern.Application.Results.Errors.Factory;

public class ErrorFactory : IErrorFactory
{
    public static IError NotFound(string message = nameof(ErrorCodes.NotFound)) => new NotFoundError(message);

    public static IError ValidationFailure(string message = nameof(ErrorCodes.ValidationFailure)) =>
        new ValidationFailureError(message);

    public static IError ValidationFailure(IEnumerable<IError> innerErrors) => new ValidationFailureError(innerErrors);

    public static IError ValidationFailure(string code, string message) => new ValidationFailureError(code, message);

    public static IError ValidationFailure(string code, string message, IEnumerable<IError> innerErrors) =>
        new ValidationFailureError(code, message, innerErrors);

    public static IError CustomError(string code, string message) => new CustomError(code, message);

    public static IError CustomError(string code, string message, IEnumerable<IError> innerErrors) =>
        new CustomError(code, message, innerErrors);
}