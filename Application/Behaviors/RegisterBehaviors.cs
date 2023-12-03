using Application.Artists.Create;
using Application.Artists.Delete;
using Application.Artists.Get;
using Application.Artists.Update;
using Application.Authorization.Login;
using Application.Authorization.Register;
using Application.Common;
using Application.DTO;
using Application.DTO.Artist;
using Application.DTO.Release;
using Application.Genres.Create;
using Application.Genres.Update;
using Application.Releases.Create;
using Application.Releases.Get;
using Application.Releases.Update;
using Application.Songs.Create;
using Application.Songs.Delete;
using Application.Songs.Update;
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
            .AddLoggingBehavior<GetReleaseQuery, ReleaseResponse>()
            .AddLoggingBehavior<CreateReleaseCommand, Guid>()
            .AddLoggingBehavior<UpdateReleaseCommand, bool>()
            .AddLoggingBehavior<DeleteArtistCommand, bool>()
            .AddLoggingBehavior<CreateSongCommand, Guid>()
            .AddLoggingBehavior<UpdateSongCommand, bool>()
            .AddLoggingBehavior<DeleteSongCommand, bool>()
            .AddLoggingBehavior<CreateGenreCommand, Guid>()
            .AddLoggingBehavior<UpdateGenreCommand, bool>();
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
            .AddValidationBehavior<RegisterCommand, string>()
            .AddValidationBehavior<CreateReleaseCommand, Guid>()
            .AddValidationBehavior<UpdateReleaseCommand, bool>()
            .AddValidationBehavior<CreateSongCommand, Guid>()
            .AddValidationBehavior<UpdateSongCommand, bool>()
            .AddValidationBehavior<CreateGenreCommand, Guid>()
            .AddLoggingBehavior<UpdateGenreCommand, bool>();
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