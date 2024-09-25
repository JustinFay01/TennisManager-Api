using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Exceptions.Handlers.Abstract;

namespace tennismanager.api.Exceptions.Handlers;

public class InternalErrorExceptionHandler : BaseExceptionHandler<InternalErrorExceptionHandler>
{
    public InternalErrorExceptionHandler(ILogger<InternalErrorExceptionHandler> logger) : base(logger)
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