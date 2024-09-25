using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Exceptions.Handlers.Abstract;

namespace tennismanager.api.Exceptions.Handlers;

public class ValidationExceptionHandler : BaseExceptionHandler<ValidationExceptionHandler>
{
    public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : base(logger)
    {
    }

    public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
            return false;

        Logger.LogError(validationException, validationException.Message, exception.Message,
            exception.InnerException?.Message);

        var problemDetails = new ProblemDetails
        {
            Title = "Validation error",
            Detail = "One or more validation errors occurred.",
            Status = 400
        };
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}