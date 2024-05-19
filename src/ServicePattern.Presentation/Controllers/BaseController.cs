using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicePattern.Application.Dtos.Result;
using ServicePattern.Application.Dtos.Result.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServicePattern.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected IActionResult HandleFailure(Result result)
    {
        if (result.IsValidationFailureResult())
            return BadRequest(
                CreateProblemDetails(
                    "Validation Error",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    result.Error.InnerErrors
                ));

        if (result.IsNotFoundResult())
            return NotFound();

        return BadRequest(
            CreateProblemDetails(
                "Validation Error",
                StatusCodes.Status400BadRequest,
                result.Error
            ));
    }

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        IError error,
        IEnumerable<IError>? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}