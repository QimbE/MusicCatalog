using Application.Data;
using HotChocolate.Execution;

namespace Application.GraphQL;

/// <summary>
/// Hot chocolate redis caching middleware
/// </summary>
internal sealed class CachingMiddleware
{
    private readonly RequestDelegate _next;

    public CachingMiddleware(
        RequestDelegate next)
    {
        _next = next ??
                throw new ArgumentNullException(nameof(next));
    }

    public async ValueTask InvokeAsync(IRequestContext context, [Service] ICacheService cache)
    {
        var key = context.Document.ToString();
        var res = await cache.GetDataAsync<Dictionary<string, object?>?>(key);
        
        var isCached = res is not null;
        
        if (isCached)
        {
            context.Result = new QueryResult(res);
        }
        
        await _next(context).ConfigureAwait(false);

        if (!isCached)
        {
            var toCache = ((QueryResult)context.Result).Data;
            await cache.SetDataAsync(key, toCache, DateTimeOffset.UtcNow.AddSeconds(30));
        }
    }
}