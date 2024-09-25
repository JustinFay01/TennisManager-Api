using Microsoft.AspNetCore.Mvc;
using tennismanager.api.ExceptionHandlers.Abstract;

namespace tennismanager.api.ExceptionHandlers;

public class ExceptionHandler : BaseExceptionHandler<ExceptionHandler>
{
    public ExceptionHandler(ILogger<ExceptionHandler> logger) : base(logger)
    {
    }

    public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        Logger.LogError(exception, $"Internal error: {exception.Message}", exception.Message,
            exception.InnerException?.Message);

        var problemDetails = new ProblemDetails
        {
            Title = "Internal error",
            Detail = "An unexpected error occurred. Please try again later.",
            Status = 500
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}