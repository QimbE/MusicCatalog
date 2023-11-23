using Application.Artists.Create;
using Application.Artists.Delete;
using Application.Artists.Get;
using Application.Artists.GetAll;
using Application.Artists.Update;
using Application.Common;
using Application.Users.Login;
using Application.Users.Register;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Behaviors;

internal static class RegisterBehaviors
{
    internal static MediatRServiceConfiguration AddPipeLineBehaviors(this MediatRServiceConfiguration config)
    {
        return config
            .AddLoggingBehaviors()
            .AddValidationBehaviors();
    }

    /// <summary>
    /// Adds all logging behaviors
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    private static MediatRServiceConfiguration AddLoggingBehaviors(this MediatRServiceConfiguration config)
    {
        return config
            .AddLoggingBehavior<CreateArtistCommand, Guid>()
            .AddLoggingBehavior<DeleteArtistCommand, bool>()
            .AddLoggingBehavior<GetArtistQuery, ArtistResponse>()
            .AddLoggingBehavior<UpdateArtistCommand, bool>()
            .AddLoggingBehavior<RegisterCommand, string>()
            .AddLoggingBehavior<LoginCommand, string>()
            .AddLoggingBehavior<GetAllArtistsQuery, IEnumerable<ArtistResponse>>();
    }

    /// <summary>
    /// Adds all validation behaviors.
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    private static MediatRServiceConfiguration AddValidationBehaviors(this MediatRServiceConfiguration config)
    {
        return config
            .AddValidationBehavior<CreateArtistCommand, Guid>()
            .AddValidationBehavior<UpdateArtistCommand, bool>()
            .AddValidationBehavior<RegisterCommand, string>();
    }

    /// <summary>
    /// Adds specific validation behavior
    /// </summary>
    /// <param name="config"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    private static MediatRServiceConfiguration AddValidationBehavior<TRequest, TResponse>(
        this MediatRServiceConfiguration config)
        where TRequest : IRequest<ResultType<TResponse>>
    {
        return config
            .AddBehavior<IPipelineBehavior<TRequest, ResultType<TResponse>>,
                ValidationPipeLineBehavior<TRequest, TResponse>>();
    }

    /// <summary>
    /// Adds specific logging behavior
    /// </summary>
    /// <param name="config"></param>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    private static MediatRServiceConfiguration AddLoggingBehavior<TRequest, TResponse>(
        this MediatRServiceConfiguration config)
        where TRequest : IRequest<ResultType<TResponse>>
    {
        return config
            .AddBehavior<IPipelineBehavior<TRequest, ResultType<TResponse>>,
                LoggingPipeLineBehavior<TRequest, TResponse>>();
    }
}