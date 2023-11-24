﻿using Application.GraphQL.TypeConfigurations;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.GraphQL;

public static class HotChocolateRegistration
{
    public static IRequestExecutorBuilder ConfigureHotChocolateTypes(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddQueryType<Endpoint>()
            .AddType<ReleaseTypeType>()
            .AddProjections()
            .AddSorting()
            .AddFiltering()
            .AddAuthorization();
    }

    public static IRequestExecutorBuilder ConfigurePipeline(this IRequestExecutorBuilder builder)
    {
        return builder
            .UseInstrumentation()
            .UseExceptions()
            .UseTimeout()
            .UseDocumentCache()
            .UseDocumentParser()
            .UseDocumentValidation()
            .UseRequest<CachingMiddleware>()
            .UseOperationCache()
            .UseOperationComplexityAnalyzer()
            .UseOperationResolver()
            .UseOperationVariableCoercion()
            .UseOperationExecution();
    }
}