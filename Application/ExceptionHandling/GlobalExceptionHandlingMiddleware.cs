using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Application.ExceptionHandling;

public class GlobalExceptionHandlingMiddleware: IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        // Can't connect to db
        catch (InvalidOperationException e)
            when (e.InnerException is NpgsqlException e2 && e2.InnerException is SocketException)
        {
            _logger.LogError(e, "An unexpected error occured: {@DevMessage}, {@ExceptionMessage}",
                " Can't connect to db server!!!", e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = CreateDetails();

            var json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        // Db server gateway
        catch (TimeoutException e)
        {
            _logger.LogError(e, "An unexpected error occured: {@DevMessage}, {@ExceptionMessage}", "Gateway timeout",
                e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.GatewayTimeout;

            ProblemDetails problem = CreateDetails(
                HttpStatusCode.GatewayTimeout,
                "Gateway Timeout",
                "Gateway Timeout",
                "The server, acting as a gateway, did not receive a timely response from the database server while processing your request."
            );

            var json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        // Operation was cancelled
        catch (OperationCanceledException e)
        {
            _logger.LogInformation(e, "Operation {@OperationName} was cancelled", context.Request.Path.ToString());
        }
        // Unknown Exception
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occured: {@DevMessage}, {@ExceptionMessage}", "Some unknown exception occured", e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = CreateDetails();

            var json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }

    private static ProblemDetails CreateDetails(HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string type = "Server error", string title = "Server error", string detail = "An internal server error has occured")
    {
        return new ProblemDetails()
        {
            Status = (int)statusCode,
            Type = type,
            Title = title,
            Detail = detail
        };
    }
}