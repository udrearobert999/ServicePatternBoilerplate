namespace ServicePattern.WebAPI.Middleware;

using Newtonsoft.Json;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var messageTitle = "Something went wrong!";

            var endPointName = context.GetEndpoint()?.DisplayName;
            if (!string.IsNullOrEmpty(endPointName))
                messageTitle = $"[{endPointName}]";

            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            }.ToString());

            _logger.Log(LogLevel.Error,
                $"{messageTitle} : {(ex.InnerException is not null ? ex.InnerException.Message : ex.Message)}");
        }
    }
}