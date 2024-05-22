﻿using ServicePattern.Domain.Results.Abstractions;
using ServicePattern.Domain.Results.Constants;

namespace ServicePattern.Domain.Results.Errors;

internal record Error : IError
{
    public string Code { get; init; } = ErrorCodes.UnknownError;

    public string Message { get; init; } = "Something went wrong!";

    public IEnumerable<IError> InnerErrors { get; set; } = Enumerable.Empty<IError>();

    protected Error(string code)
    {
        Code = code;
    }

    protected Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    protected Error(string code, string message, IEnumerable<IError> innerErrors)
    {
        Code = code;
        Message = message;
        InnerErrors = innerErrors;
    }
}