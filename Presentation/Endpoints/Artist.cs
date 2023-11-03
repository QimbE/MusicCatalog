﻿using Application.Artists.Create;
using Application.Artists.Delete;
using Application.Artists.Get;
using Application.Artists.Update;
using Application.ExpectedErrorHandling;
using Carter;
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
        var group = app.MapGroup("artists");
        
        group.MapPost("", async ([FromBody] CreateArtistCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
                );
        });

        group.MapGet("{id:guid}", async ([FromRoute] Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetArtistQuery(id));

            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success", Data = res})),
                errors => sender.Send(new HandleErrorQuery(errors))
            );
        });

        group.MapPut("{id:guid}", async ([FromRoute] Guid id, [FromBody] UpdateArtistRequest request, ISender sender) =>
            {
                var command = new UpdateArtistCommand(id, request.Name, request.Description);

                var result = await sender.Send(command);

                return await result.MatchAsync<IResult>(
                    res => Task.FromResult(Results.Ok(new {Message = "Success"})),
                    errors => sender.Send(new HandleErrorQuery(errors))
                );
            });

        group.MapDelete("{id:guid}", async ([FromRoute] Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteArtistCommand(id));

            return await result.MatchAsync<IResult>(
                res => Task.FromResult(Results.Ok(new {Message = "Success"})),
                errors => sender.Send(new HandleErrorQuery(errors))
            );
        });
    }
}