using Microsoft.Extensions.Logging;
using System.Net;

public class LoggerMiddleware
{
    private readonly ILogger<LoggerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while processing the request.");

            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}