using Microsoft.AspNetCore.Mvc;
using ServicePattern.Domain.Results;
using ServicePattern.Domain.Results.Abstractions;

namespace ServicePattern.WebAPI.Endpoints;

public static class EndpointUtils
{
    public static IResult HandleFailure(Result result)
    {
        if (result.IsSuccess && result.HasError())
            throw new InvalidOperationException("Result marked as successful but it contains error");

        if (result.IsValidationFailure())
            return Results.BadRequest(
                CreateProblemDetails(
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    result.Error.InnerErrors
                ));

        if (result.IsNotFound())
            return Results.NotFound();

        return Results.BadRequest(
            CreateProblemDetails(
                StatusCodes.Status400BadRequest,
                result.Error
            ));
    }

    private static ProblemDetails CreateProblemDetails(
        int status,
        IError error,
        IEnumerable<IError>? errors = null)
    {
        return new ProblemDetails
        {
            Type = error.Code,
            Title = "One or more validation errors occurred.",
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
    }
}