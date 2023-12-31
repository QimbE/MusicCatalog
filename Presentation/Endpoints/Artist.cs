﻿using Application.Artists.Create;
using Application.Artists.Delete;
using Application.Artists.Get;
using Application.Artists.Update;
using Application.Authorization;
using Application.ExceptionHandling.ExpectedErrors;
using Carter;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Endpoints;

public class Artist : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Artists").WithTags("Artists");
        
        group.MapPost("", async ([FromBody] CreateArtistCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command, cancellationToken);

            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
                );
        }).UserShouldBeAtLeast(Role.DatabaseAdmin);

        group.MapGet("{id:guid}", async ([FromRoute] Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetArtistQuery(id), cancellationToken);

            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
            );
        }).UserShouldBeAtLeast(Role.Default);

        group.MapPut("{id:guid}", async ([FromRoute] Guid id, [FromBody] UpdateArtistRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new UpdateArtistCommand(id, request.Name, request.Description);

                var result = await sender.Send(command, cancellationToken);

                return await result.MatchAsync<IResult>(
                    res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                    errors => sender.Send(new HandleErrorQuery(errors))
                );
            }).UserShouldBeAtLeast(Role.DatabaseAdmin);

        group.MapDelete("{id:guid}", async ([FromRoute] Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new DeleteArtistCommand(id), cancellationToken);

            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
            );
        }).UserShouldBeAtLeast(Role.DatabaseAdmin);
    }
}