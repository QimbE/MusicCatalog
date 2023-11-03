using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Presentation.Endpoints;

public class Error:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.Map("/error", (HttpContext context, ILogger<Error> logger) =>
        {
            Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint?.DisplayName;
            
            logger.LogError("Some error occured in {@endpoint}, {@exception}",endpoint, exception);
            
            (int statusCode, string message) = exception switch
            {
                OperationCanceledException e => (StatusCodes.Status400BadRequest, "Requst was cancelled"),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occured.")
            };

            return Task.FromResult(Results.Problem(statusCode: statusCode, title: message));
        });
    }
}