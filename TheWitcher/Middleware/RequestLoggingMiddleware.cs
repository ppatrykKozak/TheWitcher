using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Logowanie szczegółów żądania

        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

        // Wywołanie kolejnego middleware w pipeline

        await _next(context);

        // Logowanie szczegółów odpowiedzi

        _logger.LogInformation($"Response: {context.Response.StatusCode}");
    }
}
