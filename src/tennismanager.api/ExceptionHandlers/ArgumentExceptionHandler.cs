using Microsoft.AspNetCore.Mvc;
using tennismanager.api.ExceptionHandlers.Abstract;

namespace tennismanager.api.ExceptionHandlers;

public class ArgumentExceptionHandler : BaseExceptionHandler<ArgumentExceptionHandler>
{
    public ArgumentExceptionHandler(ILogger<ArgumentExceptionHandler> logger) : base(logger)
    {
    }


    public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ArgumentException argumentException)
            return false;

        Logger.LogError(argumentException, argumentException.Message, exception.Message,
            exception.InnerException?.Message);

        var problemDetails = new ProblemDetails
        {
            Title = "Argument error",
            Detail = "One or more argument errors occurred.",
            Status = 400
        };
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}