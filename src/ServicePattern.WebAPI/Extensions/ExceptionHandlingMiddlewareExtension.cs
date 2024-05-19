using ServicePattern.WebAPI.Middleware;

namespace ServicePattern.WebAPI.Extensions;

public static class ExceptionHandlingMiddlewareExtension
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlingMiddleware>();

        return builder;
    }
}