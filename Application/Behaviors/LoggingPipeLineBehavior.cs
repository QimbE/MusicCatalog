using Application.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public class LoggingPipeLineBehavior<TRequest, TResult>: IPipelineBehavior<TRequest, ResultType<TResult>>
    where TRequest: IRequest<ResultType<TResult>>
{
    private readonly ILogger<LoggingPipeLineBehavior<TRequest, TResult>> _logger;

    public LoggingPipeLineBehavior(ILogger<LoggingPipeLineBehavior<TRequest, TResult>> logger)
    {
        _logger = logger;
    }

    public async Task<ResultType<TResult>> Handle(
        TRequest request,
        RequestHandlerDelegate<ResultType<TResult>> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);
        
        var result = await next();

        if (result.IsError)
        {
            _logger.LogError("Requst failure {@RequestName}, {@Error} {@DateTimeUtc}",
                typeof(TRequest).Name,
                result.FirstError,
                DateTime.UtcNow);
        }
        
        _logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);
        
        return result;
    }
}