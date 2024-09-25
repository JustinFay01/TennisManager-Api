using Microsoft.AspNetCore.Diagnostics;

namespace tennismanager.api.Exceptions.Handlers.Abstract;

public abstract class BaseExceptionHandler<T> : IExceptionHandler where T : class
{
    protected readonly ILogger<T> Logger;

    protected BaseExceptionHandler(ILogger<T> logger)
    {
        Logger = logger;
    }

    public abstract ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken);
}