using System.Diagnostics;

namespace SimpleAPI.Middleware;

public sealed class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var sw = Stopwatch.StartNew();
        try
        {
            _logger.LogInformation("Incoming request {method} {path}", context.Request.Method, context.Request.Path);
            await _next(context);
            sw.Stop();
            _logger.LogInformation("Request {method} {path} responded {statusCode} in {elapsed}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                sw.Elapsed.TotalMilliseconds);
        }
        catch (Exception ex)
        {
            sw.Stop();
            _logger.LogError(ex, "Request {method} {path} failed after {elapsed}ms",
                context.Request.Method,
                context.Request.Path,
                sw.Elapsed.TotalMilliseconds);
            throw;
        }
    }
}
