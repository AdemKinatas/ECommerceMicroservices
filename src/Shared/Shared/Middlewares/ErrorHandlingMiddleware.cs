using Microsoft.AspNetCore.Http;
using NLog;
using System.Net;
using System.Text.Json;

namespace Shared.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.Error(exception, "An unhandled exception has occurred.");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            Message = "Internal Server Error",
            Details = exception.Message
        };

        var jsonResponse = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }
}
