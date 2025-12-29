using ObiletCase.Constants;
using System.Net;
using System.Text.Json;

namespace ObiletCase.Middleware;

/// <summary>
/// Global exception handling middleware
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred. Path: {Path}", context.Request.Path);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = _env.IsDevelopment()
                ? exception.Message
                : ErrorMessages.GeneralError,
            Details = _env.IsDevelopment()
                ? exception.StackTrace
                : null
        };

        // AJAX request kontrolü
        if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest" ||
            context.Request.Path.StartsWithSegments("/api"))
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        else
        {
            // Normal web request - Error sayfasına yönlendir
            context.Response.Redirect($"/Home/Error?message={Uri.EscapeDataString(ErrorMessages.GeneralError)}");
        }
    }
}

/// <summary>
/// Middleware extension methods
/// </summary>
public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionMiddleware>();
    }
}